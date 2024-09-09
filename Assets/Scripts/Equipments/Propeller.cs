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
    }
    protected override void LevelUp()
    {
        base.LevelUp();
        playerMovement.IncreaseSpeed(boostMultiplier * currentLevel);
    }
}
