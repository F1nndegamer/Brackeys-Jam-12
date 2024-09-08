using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class ShopItemSO : ScriptableObject
{
    public Sprite icon;
    public string itemName;
    public string description;
    public int price;
    public int upgradePrice;
}
public enum ItemType
{

}