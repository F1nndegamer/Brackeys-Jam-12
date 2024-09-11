using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : ShopItem
{
    [SerializeField] private float firstApplyItme = 0.2f;
    [SerializeField] private float multipleLevelUp = 0.1f;
    protected override void ApplyItem()
    {
        FishingMachanic.fishrode = firstApplyItme;
    }
    protected override void LevelUp()
    {
        base.LevelUp();
        FishingMachanic.fishrode += multipleLevelUp;
    }
}
