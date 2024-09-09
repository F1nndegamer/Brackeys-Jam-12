using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FishingMachanic : MonoBehaviour
{
    [SerializeField] private int fishdifficulty;
    [SerializeField] private List<Fish> fishList;
    private float timer = 3;
    public Dictionary<Fish, int> invantory = new Dictionary<Fish, int>();
    public bool isFishing = false;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            isFishing = !isFishing;
        Catching();
    }
    void Catching()
    {
        if (!isFishing) return;
        Fish randomFish = fishList[Random.Range(0, fishList.Count)];
        fishdifficulty = randomFish.difficulty;
        timer -=Time.deltaTime;
        if (timer < 0)
        {
            Debug.Log("catch");
            timer = (Random.value + 0.1f) * fishdifficulty;
            if (!invantory.ContainsKey(randomFish))
            {
                invantory.Add(randomFish, 1);
                Debug.Log(randomFish.difficulty);
            }
            else
            {
                invantory[randomFish]++;
            }
            isFishing = false;
        }
        else
        {
            Debug.Log("waiting");
        }
    }
}
