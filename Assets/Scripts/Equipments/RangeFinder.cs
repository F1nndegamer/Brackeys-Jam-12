using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeFinder : ShopItem
{
    protected override void ApplyItem()
    {
        GameplayUI.Instance.EnableRangeFinder();
    }
}
