using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class UpgradeShopUI : MonoBehaviour
{
    public static UpgradeShopUI Instance;
    [SerializeField] private TextMeshProUGUI moneyText;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {   
        UpdateUI();
    }
    public void UpdateUI()
    {
        moneyText.text = Player.Instance.Money.ToString();
    }
    public bool BuyItem(ShopItem item)
    {
        if (Player.Instance.Money >= item.BuyPrice)
        {
            Player.Instance.UpdateMoney(-item.BuyPrice);
            
            UpdateUI();
            return true;
        }
        UpdateUI();
        return false;
    }
    public bool UpgradeItem(ShopItem item)
    {
        if (Player.Instance.Money >= item.CurrentUpgradePrice)
        {
            Player.Instance.UpdateMoney(-item.CurrentUpgradePrice);

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
                break;
            case ItemType.ThirdItem:
                break;
        }
    }
}
