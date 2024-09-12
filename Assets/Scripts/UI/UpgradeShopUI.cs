using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        Hide();
    }
    private void OnEnable()
    {
        UpdateUI();
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
        if (Player.Instance != null)
        {
            moneyText.text = Player.Instance.Money.ToString();
        }
    }
    public bool BuyItem(ShopItem item)
    {
        if (Player.Instance.Money >= item.BuyPrice)
        {
            Player.Instance.UpdateMoney(-item.BuyPrice);
            SoundManager.Instance.PlaySpendMoneySound();

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
            SoundManager.Instance.PlaySpendMoneySound();

            UpdateUI();
            return true;
        }
        UpdateUI();
        return false;
    }
    private void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    private void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
