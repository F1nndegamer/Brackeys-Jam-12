using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
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
        }
    }
    public void UpdateMoney(int money)
    {
        Money += money;
    }
}
