using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeShopUI : MonoBehaviour
{
    [SerializeField] private ShopItem[] items;
    [SerializeField] private TextMeshProUGUI moneyText;
    private void UpdateUI()
    {
        moneyText.text = Player.Instance.Money.ToString();
    }
    public void BuyUpgrade(ItemType upgradeIndex)
    {
        ShopItem upgrade = items[(int)upgradeIndex];
        if (Player.Instance.Money >= upgrade.GetShopItemSO().price)
        {
            Player.Instance.UpdateMoney(-upgrade.GetShopItemSO().price);
            upgrade.LevelUp();
        }
    }
}
