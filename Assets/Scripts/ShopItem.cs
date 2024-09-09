using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum ItemType
{
    FishingRod = 0,
    RangeFinder = 1,
    Propeller = 2,
    ThirdItem = 3,
}
public class ShopItem : MonoBehaviour
{
    [SerializeField] private Button purchaseButton;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TextMeshProUGUI upgradePriceText;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private int buyPrice;
    [SerializeField] private int[] upgradePriceArray;
    public int BuyPrice => buyPrice;
    public int[] UpgradePriceArray => upgradePriceArray;
    public int CurrentUpgradePrice => upgradePriceArray[currentLevel-1];

    private string itemName;
    private readonly Dictionary<int, string> ROMAN_NUMERALS = new Dictionary<int, string>() { { 1, "I"}, { 2, "II"}, { 3, "III"}, { 4, "IV" }, { 5, "V" } };
    protected bool isBought;
    protected int currentLevel = 1;
    private void Awake()
    {
        itemName = nameText.text;
    }
    private void Start()
    {
        UpdateItem();
        nameText.text = itemName + ROMAN_NUMERALS[currentLevel];

        purchaseButton.onClick.AddListener(() =>
        {
            if (UpgradeShopUI.Instance.BuyItem(this))
            {
                isBought = true;
                ApplyItem();
                UpdateItem();
            }
        });
        upgradeButton.onClick.AddListener(() =>
        {
            if (UpgradeShopUI.Instance.UpgradeItem(this))
            {
                LevelUp();
                UpdateItem();
            }
        });

    }
    private void UpdateItem()
    {
        purchaseButton.interactable = !isBought;
        if (upgradePriceArray.Length > 0 && currentLevel < 4 && CurrentUpgradePrice > 0)
        {
            upgradePriceText.text = CurrentUpgradePrice.ToString();
        }
        else
        {
            upgradePriceText.text = "Max";
            upgradeButton.interactable = false;
        }
        if (isBought)
        {
            priceText.text = "Purchased";
            purchaseButton.interactable = false;
        }
        else
        {
            priceText.text = buyPrice.ToString();
        }
    }
    protected virtual void ApplyItem()
    {
    }
    protected virtual void LevelUp()
    {
        currentLevel++;
        nameText.text = itemName + ROMAN_NUMERALS[currentLevel];
    }
}
