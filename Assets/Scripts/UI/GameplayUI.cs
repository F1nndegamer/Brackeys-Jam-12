using System.Linq;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI Instance;
    [SerializeField] private FishingMachanic fishingMechanic;
    [SerializeField] private TextMeshProUGUI numberOfFishText;
    [SerializeField] private TextMeshProUGUI fishCaughtNotificationText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Animator notificationAnimator;
    [SerializeField] private Animator resetSequenceAnimator;
    [SerializeField] private GameObject multiPurposeMeter;
    private RangeFinderInformation rangeFinderInformation;
    private void Awake()
    {
        Instance = this;
        rangeFinderInformation = GetComponent<RangeFinderInformation>();
    }
    private void Start()
    {
        Player.Instance.OnMoneyChanged += Player_OnMoneyChanged;
        fishingMechanic.OnFishCaught += FishingMechanic_OnFishCaught;
        fishingMechanic.OnFishSold += FishingMechanic_OnFishSold;
        rangeFinderInformation.enabled = false;
        multiPurposeMeter.SetActive(false);
    }

    private void Player_OnMoneyChanged(object sender, System.EventArgs e)
    {
        UpdatePlayerMoney();
    }

    public void UpdatePlayerMoney()
    {
        moneyText.text = Player.Instance.Money.ToString();   
    }
    private void FishingMechanic_OnFishSold(object sender, System.EventArgs e)
    {
        int totalFishSold = InventoryUI.Instance.basket.Sum(x => x.Value.count);
        if (totalFishSold == 0) return;
        numberOfFishText.text = "0";
        int totalEarnings = InventoryUI.Instance.basket.Sum(x => x.Value.scaledPriceList.Sum() * x.Value.count);
        ShowNotification($"Sold {totalFishSold} fish for {totalEarnings} currency!");
    }
    private void FishingMechanic_OnFishCaught(object sender, FishingMachanic.OnFishCaughtEventArgs e)
    {
        numberOfFishText.text = InventoryUI.Instance.basket.Sum(x => x.Value.count).ToString();
        ShowNotification("Caught " + e.fishSO.fishName + "!");
    }
    public void ResetNumberOfFish()
    {
        numberOfFishText.text = "0";
    }
    public void ShowNotification(string message)
    {
        fishCaughtNotificationText.text = message;
        fishCaughtNotificationText.gameObject.SetActive(true);
        notificationAnimator.SetTrigger("SlideIn");
        CancelInvoke(nameof(NotificationSlideOut));
        Invoke(nameof(NotificationSlideOut), 3f);
    }
    public void ShowDeathSequence()
    {
        resetSequenceAnimator.gameObject.SetActive(true);
        resetSequenceAnimator.SetTrigger("Death");
        Invoke(nameof(ShowRespawnSequence), 3f);
    }
    private void ShowRespawnSequence()
    {
        resetSequenceAnimator.SetTrigger("Respawn");
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
