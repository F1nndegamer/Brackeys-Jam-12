using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : ShopItem
{
    [SerializeField] private float firstApplyTime = 1f;
    [SerializeField] private float levelUpIncrement = 0.75f;
    [SerializeField] private FishingMachanic fishingMechanic;
    protected override void ApplyItem()
    {
        fishingMechanic.FishRodReductionTime = firstApplyTime;
    }
    protected override void LevelUp()
    {
        base.LevelUp();
        fishingMechanic.FishRodReductionTime += levelUpIncrement;
    }
}
