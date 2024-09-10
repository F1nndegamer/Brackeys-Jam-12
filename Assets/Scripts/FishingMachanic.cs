using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingMachanic : MonoBehaviour
{
    int y = 1;
    [SerializeField] private List<FishSO> fishList;
    public bool isFishing = false;  
    FishSO randomFish;
    public GameObject bar;
    public static int fishrode;
    public static string lastFishCaughtName;
    Transform greenBar;
    Transform whiteBar; 
    private void Start()
    {
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
        if (Input.GetMouseButtonDown(1) && isFishing)
        {
            Catching(randomFish);
            isFishing = false;
        }
        if (isFishing)
        {
            bar.SetActive(true);
            WhiteBarMove(Mathf.Log(randomFish.whiteBarSpeed));
        }
        else
        {
            bar.SetActive(false);
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
    public int FishingDifficulty(int fishsdif, int fisrodebuff = 0)
    {
        int res = 0;
        res = fishsdif - fisrodebuff;
        return res;
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
