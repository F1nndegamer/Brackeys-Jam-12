using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private int fishCaught;
    public int Money { get; private set; } 
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
    }
    public void UpdateMoney(int money)
    {
        Money += money;
    }
}
