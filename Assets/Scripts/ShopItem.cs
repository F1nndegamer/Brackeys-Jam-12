using System.Collections;
using System.Collections.Generic;
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
        itemNameText.text = shopItemSO.itemName;
        descriptionText.text = shopItemSO.description;
        icon.sprite = shopItemSO.icon;
        priceText.text = shopItemSO.price.ToString();
        upgradePriceText.text = shopItemSO.upgradePrice.ToString();

        upgradeButton.interactable = false;
        purchaseButton.onClick.AddListener(() =>
        {
            isBought = true;
            UpgradeShopUI.Instance.BuyItem(shopItemSO.itemType);
            upgradeButton.interactable = isBought;
        });
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
