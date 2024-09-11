using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingMachanic : MonoBehaviour
{
    public bool isWaitingForFish = false;  
    public bool isCatching = false;  
    public bool IsFishing => isWaitingForFish || isCatching;
    public static int fishrode;
    public static string lastFishCaughtName;
    public static Dictionary<FishSO, int> basket = new Dictionary<FishSO, int>();
    [SerializeField] private List<FishSO> fishList;
    [SerializeField] private int waitingTimerMax = 3;
    [SerializeField] private RectTransform bar;
    [SerializeField] private RectTransform sweetSpot;
    [SerializeField] private RectTransform whitePointer;
    private int pointerDirection = 1;
    private int currentCatchProgress = 0;  
    private FishSO currentFish;
    private float waitingTimer;
    private float barLength;
    private void Start()
    {
        barLength = bar.rect.width;
        waitingTimer = UnityEngine.Random.Range(1, waitingTimerMax);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isWaitingForFish && !isCatching)
        {
            currentFish = fishList[UnityEngine.Random.Range(0, fishList.Count)];
            InitializeGreenBar(currentFish);
            currentCatchProgress = 0;
            isWaitingForFish = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isCatching)
        {
            Catching(currentFish);
            isCatching = false;
            isWaitingForFish = false;
        }
        if (isCatching)
        {
            bar.gameObject.SetActive(true);
            MovePointer(currentFish.pointerSpeed);
        }
        else
        {
            bar.gameObject.SetActive(false);
        }
        if (isWaitingForFish)
        {
            waitingTimer -= Time.deltaTime;
            if (waitingTimer < 0)
            {
                isCatching = true;
                isWaitingForFish = false;
                waitingTimer = UnityEngine.Random.Range(1, waitingTimerMax); 
            }
        }
    }
    private void Catching(FishSO randfish)
    {
        float halfSweetSpotWidth = sweetSpot.sizeDelta.x / 2;
        float sweetSpotCenterX = sweetSpot.anchoredPosition.x;

        // Check if the white pointer is within the sweet spot's bounds
        if (whitePointer.localPosition.x > (sweetSpotCenterX - halfSweetSpotWidth)
            && whitePointer.localPosition.x < (sweetSpotCenterX + halfSweetSpotWidth))
        {
            Debug.Log("Catch attempt successful");

            currentCatchProgress++;  // Increment catch progress

            // Check if player has caught the fish the required number of times
            if (currentCatchProgress >= randfish.requiredCatches)
            {
                Debug.Log("Fish caught successfully!");

                if (!basket.ContainsKey(randfish))
                {
                    basket.Add(randfish, 1);
                }
                else
                {
                    basket[randfish]++;
                }

                lastFishCaughtName = randfish.name;

                // Reset the progress after catching the fish
                currentCatchProgress = 0;
                isCatching = false;  // End the catching phase
                isWaitingForFish = false;  // Ready for the next fish
            }
        }
    }


    private void InitializeGreenBar(FishSO randfish)
    {
        sweetSpot.sizeDelta = new Vector2(randfish.sweetSpotLength * barLength, sweetSpot.sizeDelta.y);

        // Calculate the minimum and maximum positions for the sweet spot to ensure it stays within the bounds of the bar
        float halfBarLength = barLength / 2;
        float halfSweetSpotWidth = sweetSpot.sizeDelta.x / 2;

        // Adjust the range so the green bar doesn't go outside
        float minPosition = -halfBarLength + halfSweetSpotWidth;
        float maxPosition = halfBarLength - halfSweetSpotWidth;

        sweetSpot.anchoredPosition = new Vector2(UnityEngine.Random.Range(minPosition, maxPosition), 0);
    }
    private void MovePointer(float whiteBarSpeed)
    {
        if (whitePointer.anchoredPosition.x >= barLength/2)
        {
            pointerDirection = -1;
        }
        else if (whitePointer.anchoredPosition.x <= -barLength / 2)
        {
            pointerDirection = 1;
        }
        whitePointer.anchoredPosition = new Vector2(whitePointer.anchoredPosition.x + whiteBarSpeed * pointerDirection * Time.deltaTime, 0);
    }
}
