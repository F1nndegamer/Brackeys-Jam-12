using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : ShopItem
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float boostMultiplier;
    protected override void ApplyItem()
    {
        playerMovement.IncreaseSpeed(boostMultiplier * currentLevel);
        StormManager.instance.stormSpeed += boostMultiplier * currentLevel;
        StormManager.instance.outrunDistance += 10f;
        InventoryUI.Instance.PropellerItem(true, "increases the speed of the boat", currentLevel);
    }
    protected override void LevelUp()
    {
        base.LevelUp();
        playerMovement.IncreaseSpeed(boostMultiplier * currentLevel);
        StormManager.instance.stormSpeed += boostMultiplier * currentLevel;
        StormManager.instance.outrunDistance += 10f;
        InventoryUI.Instance.PropellerItem(true, "increases the speed of the boat", currentLevel);
    }
}
