using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class ShopItemSO : ScriptableObject
{
    public ItemType itemType;
    public Sprite icon;
    public string itemName;
    public string description;
    public int price;
    public int upgradePrice;
}
public enum ItemType
{
    FishingRod = 0,
    RangeFinder = 1,
    SecondItem = 2,
    ThirdItem = 3,
}