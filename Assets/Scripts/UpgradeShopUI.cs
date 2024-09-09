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
    private void Start()
    {   
        UpdateUI();
    }
    private void UpdateUI()
    {
        moneyText.text = Player.Instance.Money.ToString();
    }
    public bool BuyItem(ShopItem shopItem)
    {
        if (Player.Instance.Money >= shopItem.GetShopItemSO().price)
        {
            Debug.Log(Player.Instance.Money + " - " + shopItem.GetShopItemSO().price);
            Player.Instance.UpdateMoney(-shopItem.GetShopItemSO().price);
            ApplyItem(shopItem.GetShopItemSO().itemType);
            shopItem.LevelUp();
            UpdateUI();
            return true;
        }
        UpdateUI();
        return false;
    }
    private void ApplyItem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.FishingRod:
                break;
            case ItemType.RangeFinder:
                GameplayUI.Instance.EnableRangeFinder();
                break;
            case ItemType.ThirdItem:
                break;
        }
    }
}
