using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private ShopItemSO shopItemSO;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private Button upgradeButton;
    private int currentLevel = 1;
    public void LevelUp()
    {
        currentLevel++;
    }
    public ShopItemSO GetShopItemSO()
    {
        return shopItemSO;
    }
}
