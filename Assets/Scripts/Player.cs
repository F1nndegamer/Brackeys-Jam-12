using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public static Player Instance;
    public event EventHandler OnMoneyChanged;
    public event EventHandler OnShopOpened;
    public event EventHandler OnShopClosed;
    public event EventHandler OnInventoryOpened;
    public event EventHandler OnInvetoryClosed;
    private int fishCaught;
    public int Money { get; private set; }
    public bool isNearShop = true;
    private bool isShopOpened = false;
    private bool isInventoryOpened = false;
    private Animator animator;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            UpdateMoney(100);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleShop();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
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
        OnMoneyChanged?.Invoke(this, EventArgs.Empty);
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
    //public async void Death(bool isDeath)
    //{
    //    await UniTask.WaitUntil(() => isDeath);
    //    animator.enabled = true;
    //    animator.SetBool("isDeath", true);
    //    await UniTask.Delay(1000); //after the animation is over
    //    animator.SetBool("isDeath", false);
    //    await UniTask.Delay(10);
    //    animator.enabled = false;
    //    isDeath = false;
    //    transform.position = new Vector2(0, 0);
    //    GetComponent<FishingMachanic>().isCatching = false;
    //    GetComponent<FishingMachanic>().isWaitingForFish = false;
    //    GetComponent<FishingMachanic>().EndCatchingFish();
    //    //change sound
    //}
}
