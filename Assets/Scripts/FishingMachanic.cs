using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FishingMachanic : MonoBehaviour
{
    public event EventHandler<OnFishCaughtEventArgs> OnFishCaught;
    public class OnFishCaughtEventArgs : EventArgs
    {
        public FishSO fishSO;
    }

    public event EventHandler OnFishSold;

    public bool IsFishing => isWaitingForFish || isCatching;
    public float FishRodReductionTime;

    [SerializeField] private List<FishSO> fishList;
    [SerializeField] private int waitingTimerMax = 8;
    [SerializeField] private int waitingTimerMin = 4;
    [SerializeField] private RectTransform bar;
    [SerializeField] private RectTransform sweetSpot;
    [SerializeField] private RectTransform whitePointer;
    [SerializeField] private TextMeshProUGUI catchProgressText;
    [SerializeField] private GameObject zoneBoundary;

    private bool isInSafeZone = true;
    private bool isCatching = false;
    private bool isWaitingForFish = false;
    private int pointerDirection = 1;
    private int currentCatchProgress = 0;
    private FishSO currentFish;
    private float waitingTimer;
    private float barLength;

    private void Start()
    {
        barLength = bar.rect.width;
        GenerateWaitingTimer();
    }

    private void Update()
    {
        CheckZone();
        if (Input.GetKeyDown(KeyCode.Space) && !isWaitingForFish && !isCatching)
        {
            if (!isInSafeZone)
            {
                WaitForFish();
            }
            else
            {
                Debug.Log("Cannot Fish In The Safe Zone");
            }
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
                SoundManager.Instance.PlayFishEngageSound();
                GenerateWaitingTimer();
            }
        }
        CheckZone(); // Check the player's zone status
    }

    private void WaitForFish()
    {
        isWaitingForFish = true;
        currentFish = fishList[Random.Range(0, fishList.Count)];
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

                InventoryUI.Instance.UpdateUI(currentFish);

                FishingMinigameUI.Instance.Flash();
                OnFishCaught?.Invoke(this, new OnFishCaughtEventArgs
                {
                    fishSO = currentFish
                });
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

    private void CheckZone()
    {
        if (!isInSafeZone && transform.position.x <= zoneBoundary.transform.position.x)
        {
            // Player crossed into the safe zone
            isInSafeZone = true;
            SellAllFish();
        }
        else if (isInSafeZone && transform.position.x > zoneBoundary.transform.position.x)
        {
            // Player crossed back into the danger zone
            isInSafeZone = false;
        }
    }

    private void SellAllFish()
    {
        Debug.Log("All Fish Sold");
        OnFishSold?.Invoke(this, EventArgs.Empty);
        InventoryUI.Instance.SellAllFish();
    }
    private void StartCatchingProcess()
    {
        isCatching = true;
        isWaitingForFish = false;
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

        // Adjust the range so the sweet spot doesn't go outside
        float minPosition = -halfBarLength + halfSweetSpotWidth;
        float maxPosition = halfBarLength - halfSweetSpotWidth;

        sweetSpot.anchoredPosition = new Vector2(UnityEngine.Random.Range(minPosition, maxPosition), 0);
    }

    private void MovePointer()
    {
        if (whitePointer.anchoredPosition.x >= barLength / 2)
        {
            pointerDirection = -1;
        }
        else if (whitePointer.anchoredPosition.x <= -barLength / 2)
        {
            pointerDirection = 1;
        }
        whitePointer.anchoredPosition = new Vector2(whitePointer.anchoredPosition.x + currentFish.pointerSpeed * pointerDirection * Time.deltaTime, 0);
    }

    private void GenerateWaitingTimer()
    {
        waitingTimer = Random.Range(waitingTimerMin, waitingTimerMax) - FishRodReductionTime;
        if (waitingTimer < 0)
        {
            waitingTimer = 0f;
        }
    }
}