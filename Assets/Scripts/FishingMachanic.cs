using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingMachanic : MonoBehaviour
{
    [SerializeField] private List<Fish> fishList;
    private float timer = 3;
    public bool isFishing = false;
    public static int fishrode;
    public static string lastFishCaughtName;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isFishing = !isFishing;
        }
        Catching();
    }
    void Catching()
    {
        if (!isFishing) return;
        Fish randomFish = fishList[Random.Range(0, fishList.Count)];
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Debug.Log("catch");
            timer = (Random.value + 0.1f) * FishingDifficulty(randomFish.difficulty, fishrode); 
            //Player.Instance.UpdateMoney(randomFish.price);
            lastFishCaughtName = randomFish.name;
            isFishing = false;
        }
        else
        {
            Debug.Log("waiting");
        }
    }
    public int FishingDifficulty(int fishsdif, int fisrodebuff = 0)
    {
        int res = 0;
        res = fishsdif - fisrodebuff;
        return res;
    }
}
