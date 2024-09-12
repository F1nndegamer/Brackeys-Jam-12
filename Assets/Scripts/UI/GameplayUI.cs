using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI Instance;
    [SerializeField] private FishingMachanic fishingMechanic;
    [SerializeField] private TextMeshProUGUI numberOfFishText;
    [SerializeField] private TextMeshProUGUI fishCaughtNotificationText;
    [SerializeField] private Animator notificationAnimator;
    [SerializeField] private GameObject multiPurposeMeter;
    private RangeFinderInformation rangeFinderInformation;
    private void Awake()
    {
        Instance = this;
        rangeFinderInformation = GetComponent<RangeFinderInformation>();
    }
    private void Start()
    {
        fishingMechanic.OnFishCaught += FishingMechanic_OnFishCaught;
        fishingMechanic.OnFishSold += FishingMechanic_OnFishSold;
        rangeFinderInformation.enabled = false;
        multiPurposeMeter.SetActive(false);
    }

    private void FishingMechanic_OnFishSold(object sender, System.EventArgs e)
    {
        int totalFishSold = InventoryUI.basket.Sum(x => x.Value);
        if (totalFishSold == 0) return;
        numberOfFishText.text = "0";
        int totalEarnings = InventoryUI.basket.Sum(x => x.Key.price * x.Value);
        fishCaughtNotificationText.text = $"Sold {totalFishSold} fish for {totalEarnings} currency!";
        fishCaughtNotificationText.gameObject.SetActive(true);
        notificationAnimator.SetTrigger("SlideIn");
        Invoke(nameof(NotificationSlideOut), 3f);
    }

    private void FishingMechanic_OnFishCaught(object sender, FishingMachanic.OnFishCaughtEventArgs e)
    {
        numberOfFishText.text = InventoryUI.basket.Sum(x => x.Value).ToString();
        fishCaughtNotificationText.text = "Caught " + e.fishSO.fishName + "!";
        fishCaughtNotificationText.gameObject.SetActive(true);
        notificationAnimator.SetTrigger("SlideIn");
        Invoke(nameof(NotificationSlideOut), 3f);
    }
    private void NotificationSlideOut()
    {
        notificationAnimator.SetTrigger("SlideOut");
    }
    public void EnableRangeFinder()
    {
        rangeFinderInformation.enabled = true;
    }
    public void MultiPurposeMeter()
    {
        multiPurposeMeter.SetActive(true);
    }
}
