using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingMachanic : MonoBehaviour
{
    public bool isFishing = false;  
    public bool isCatching = false;  
    public GameObject bar;
    public static int fishrode;
    public static string lastFishCaughtName;
    int y = 1;
    [SerializeField] private List<FishSO> fishList;
    [SerializeField] private int randomRange = 3;
    FishSO randomFish;
    Transform greenBar;
    Transform whiteBar;
    float timer;
    private void Start()
    {
        timer = UnityEngine.Random.value * 3;
        greenBar = bar.transform.GetChild(0);
        whiteBar = bar.transform.GetChild(1);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isFishing)
        {
            randomFish = fishList[UnityEngine.Random.Range(0, fishList.Count)];
            isFishing = true;
        }
        if (Input.GetMouseButtonDown(1) && isCatching)
        {
            Catching(randomFish);
            isCatching = false;
            isFishing = false;
        }
        if (isCatching)
        {
            bar.SetActive(true);
            WhiteBarMove(randomFish.whiteBarSpeed);
        }
        else
        {
            bar.SetActive(false);
        }
        if (isFishing)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                isCatching = true;
                timer = UnityEngine.Random.value * randomRange; 
            }
        }
    }
    void Catching(FishSO randfish)
    {
        if (!isFishing) return;
        greenBar.localScale = new Vector2(randfish.greenBarLong, 1);
        if (whiteBar.localPosition.x > -(greenBar.localScale.x / 2 - 0.03f) && whiteBar.localPosition.x < (greenBar.localScale.x / 2 - 0.03f))
        {
            //Player.Instance.UpdateMoney(randfish.price);
            lastFishCaughtName = randfish.name;
        }
    }
    void WhiteBarMove(float whiteBarSpeed)
    {
        if (whiteBar.localPosition.x >= 0.5f || whiteBar.localPosition.x <= -0.5f)
        {
            y *= -1;
        }
        whiteBar.localPosition = new Vector2(whiteBar.localPosition.x + whiteBarSpeed * y * Time.deltaTime, 0);
    }
}
