using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Tilemaps;
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
        Player.Instance.OnShopOpened += Player_OnShopOpened;
        Player.Instance.OnShopClosed += Player_OnShopClosed;
        UpdateUI();
        Hide();
    }

    private void Player_OnShopClosed(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Player_OnShopOpened(object sender, System.EventArgs e)
    {
        Show();
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
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
}
