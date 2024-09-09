using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private ShopItemSO shopItemSO;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TextMeshProUGUI upgradePriceText;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image icon;
    private bool isBought;
    private int currentLevel = 1;
    private void Start()
    {
        InitializeItem();
        UpdateItem();

        purchaseButton.onClick.AddListener(() =>
        {
            if (UpgradeShopUI.Instance.BuyItem(this))
            {
                isBought = true;
                UpdateItem();
            }
        });
    }
    private void InitializeItem()
    {
        itemNameText.text = shopItemSO.itemName;
        descriptionText.text = shopItemSO.description;
        icon.sprite = shopItemSO.icon;
    }
    private void UpdateItem()
    {
        purchaseButton.interactable = !isBought;
        if (shopItemSO.upgradePrice > 0)
        {
            upgradePriceText.text = shopItemSO.upgradePrice.ToString();
        }
        else
        {
            upgradePriceText.text = "No Upgrade";
            upgradeButton.interactable = false;
        }
        if (isBought)
        {
            priceText.text = "Purchased";
            purchaseButton.interactable = false;
        }
        else
        {
            priceText.text = shopItemSO.price.ToString();
        }
    }
    public void LevelUp()
    {
        currentLevel++;
    }
    public ShopItemSO GetShopItemSO()
    {
        return shopItemSO;
    }
}
