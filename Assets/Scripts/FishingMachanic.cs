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
        timer -=Time.deltaTime;
        if (timer < 0)
        {
            Debug.Log("catch");
            timer = (Random.value + 0.1f) * fishdifficulty;
            isFishing = false;
        }
        else
        {
            Debug.Log("waiting");
        }
    }
    IEnumerator fishWaiting()
    {
        Debug.Log("waiting");
        yield return new WaitForSeconds((Random.value + 0.1f) * fishdifficulty);
        Debug.Log("catch");
        Fish randomFish = fishList[Random.Range(0, fishList.Count)];
        isFishing = false;
    }
}
