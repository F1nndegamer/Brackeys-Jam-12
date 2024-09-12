using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPurposeMeter : ShopItem
{
    protected override void ApplyItem()
    {
        base.ApplyItem();
        GameplayUI.Instance.MultiPurposeMeter();
        InventoryUI.Instance.MultiPurposeMeterItem(true, "foretells the storm", currentLevel);
    }
}
