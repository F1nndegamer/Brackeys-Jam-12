using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FishingMachanic : MonoBehaviour
{
    public event EventHandler OnFishCaught;
    public bool isWaitingForFish = false;  
    public bool isCatching = false;  
    public bool IsFishing => isWaitingForFish || isCatching;
    public static float fishrode;
    public static string lastFishCaughtName;
    public static Dictionary<FishSO, int> basket = new Dictionary<FishSO, int>();
    [SerializeField] private List<FishSO> fishList;
    [SerializeField] private int waitingTimerMax = 8;
    [SerializeField] private int waitingTimerMin = 2;
    [SerializeField] private RectTransform bar;
    [SerializeField] private RectTransform sweetSpot;
    [SerializeField] private RectTransform whitePointer;
    [SerializeField] private TextMeshProUGUI catchProgressText;
    
    private int pointerDirection = 1;
    private int currentCatchProgress = 0;  
    private FishSO currentFish;
    private float waitingTimer;
    private float barLength;
    private void Start()
    {
        barLength = bar.rect.width;
        waitingTimer = UnityEngine.Random.Range(waitingTimerMin, waitingTimerMax) - fishrode;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isWaitingForFish && !isCatching)
        {
            WaitForFish();
            FishQualityCalculation();
        }
        if (Input.GetKeyDown(KeyCode.Space) && isCatching && currentCatchProgress <= currentFish.requiredCatches)
        {
            AttemptToCatchFish();
        }
        if (isCatching)
        {
            MovePointer();
        }
        if (isWaitingForFish)
        {
            waitingTimer -= Time.deltaTime;
            if (waitingTimer < 0)
            {
                StartCatchingProcess();
            }
        }
    }
    private void WaitForFish()
    {
        isWaitingForFish = true;
        currentFish = fishList[UnityEngine.Random.Range(0, fishList.Count)];
        InitializeSweetSpot();
        currentCatchProgress = 0;
    }
    private void AttemptToCatchFish()
    {
        float halfSweetSpotWidth = sweetSpot.sizeDelta.x / 2;
        float sweetSpotCenterX = sweetSpot.anchoredPosition.x;

        // Check if the white pointer is within the sweet spot's bounds
        if (whitePointer.localPosition.x > (sweetSpotCenterX - halfSweetSpotWidth)
            && whitePointer.localPosition.x < (sweetSpotCenterX + halfSweetSpotWidth))
        {
            Debug.Log("Catch attempt successful");

            currentCatchProgress++;
            FishingMinigameUI.Instance.Flash();
            catchProgressText.text = currentCatchProgress.ToString() + "/" + currentFish.requiredCatches.ToString();
            if (currentCatchProgress >= currentFish.requiredCatches)
            {
                Debug.Log("Fish caught successfully!");

                if (!basket.ContainsKey(currentFish))
                {
                    basket.Add(currentFish, 1);
                }
                else
                {
                    basket[currentFish]++;
                }

                lastFishCaughtName = currentFish.name;
                FishingMinigameUI.Instance.Flash();
                OnFishCaught?.Invoke(this, EventArgs.Empty);
                EndCatchingFish();
            }
            else
            {
                InitializeSweetSpot();
            }
        }
        else
        {
            EndCatchingFish();
        }
    }
    private void StartCatchingProcess()
    {
        isCatching = true;
        isWaitingForFish = false;
        waitingTimer = UnityEngine.Random.Range(1, waitingTimerMax) - fishrode;
        bar.gameObject.SetActive(true);
        FishingMinigameUI.Instance.FadeIn();
        catchProgressText.text = currentCatchProgress.ToString() + "/" + currentFish.requiredCatches.ToString();
    }
    private void EndCatchingFish()
    {
        currentCatchProgress = 0;
        isCatching = false;
        FishingMinigameUI.Instance.FadeOut();
    }

    private void InitializeSweetSpot()
    {
        sweetSpot.sizeDelta = new Vector2(currentFish.sweetSpotLength * barLength, sweetSpot.sizeDelta.y);

        // Calculate the minimum and maximum positions for the sweet spot to ensure it stays within the bounds of the bar
        float halfBarLength = barLength / 2;
        float halfSweetSpotWidth = sweetSpot.sizeDelta.x / 2;

        // Adjust the range so the green bar doesn't go outside
        float minPosition = -halfBarLength + halfSweetSpotWidth;
        float maxPosition = halfBarLength - halfSweetSpotWidth;

        sweetSpot.anchoredPosition = new Vector2(UnityEngine.Random.Range(minPosition, maxPosition), 0);
    }
    private void MovePointer()
    {
        if (whitePointer.anchoredPosition.x >= barLength/2)
        {
            pointerDirection = -1;
        }
        else if (whitePointer.anchoredPosition.x <= -barLength / 2)
        {
            pointerDirection = 1;
        }
        whitePointer.anchoredPosition = new Vector2(whitePointer.anchoredPosition.x + currentFish.pointerSpeed * pointerDirection * Time.deltaTime, 0);
    }
    private void FishQualityCalculation()
    {
        currentFish.price += UnityEngine.Random.Range(0, ((int)MathF.Log10(RangeFinderInformation.distance)) * 10);
    }
}