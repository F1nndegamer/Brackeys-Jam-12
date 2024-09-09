using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeShopUI : MonoBehaviour
{
    public static UpgradeShopUI Instance;
    [SerializeField] private ShopItem[] itemArray;
    [SerializeField] private TextMeshProUGUI moneyText;
    private void Awake()
    {
        Instance = this;
    }
    private void UpdateUI()
    {
        moneyText.text = Player.Instance.Money.ToString();
    }
    public void BuyItem(ItemType itemType)
    {
        ShopItem shopItem = itemArray[(int)itemType];
        if (Player.Instance.Money >= shopItem.GetShopItemSO().price)
        {
            Player.Instance.UpdateMoney(-shopItem.GetShopItemSO().price);
            ApplyItem(itemType);
            shopItem.LevelUp();
        }
        UpdateUI();
    }
    private void ApplyItem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.FishingRod:
                break;
            case ItemType.SecondItem:
                break;
            case ItemType.ThirdItem:
                break;
        }
    }
}
