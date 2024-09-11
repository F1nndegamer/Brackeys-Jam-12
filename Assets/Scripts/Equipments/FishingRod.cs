using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : ShopItem
{
    protected override void ApplyItem()
    {
        FishingMachanic.fishrode = 0.2f;
    }
    protected override void LevelUp()
    {
        base.LevelUp();
        FishingMachanic.fishrode += 0.1f;
    }
}
