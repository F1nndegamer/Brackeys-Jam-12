using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeFinder : ShopItem
{
    protected override void ApplyItem()
    {
        GameplayUI.Instance.EnableRangeFinder();
        InventoryUI.Instance.RangeFidnerItem(true, "shows the distance to the shore", currentLevel);
    }
}
