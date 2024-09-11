using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public event EventHandler OnShopOpened;
    public event EventHandler OnShopClosed;
    public event EventHandler OnInventoryOpened;
    public event EventHandler OnInvetoryClosed;
    private int fishCaught;
    public int Money { get; private set; }
    private bool isNearShop = true;
    private bool isShopOpened = false;
    private bool isInventoryOpened = false;
    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            UpdateMoney(100);
            UpgradeShopUI.Instance.UpdateUI();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleShop();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
        if (!isNearShop && isShopOpened)
        {
            isShopOpened = !isShopOpened;
            OnShopClosed?.Invoke(this, EventArgs.Empty);
        }
    }
    public void UpdateMoney(int money)
    {
        Money += money;
    }
    private void ToggleShop()
    {   
        if (isNearShop)
        {
            isShopOpened = !isShopOpened;
            if (isShopOpened)
            {
                OnShopOpened?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnShopClosed?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public void ToggleInventory()
    {
        isInventoryOpened = !isInventoryOpened;
        if (isInventoryOpened)
        {
            OnInventoryOpened?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnInvetoryClosed?.Invoke(this, EventArgs.Empty);
        }
    }
}
