using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingMachanic : MonoBehaviour
{
    [Header("Fish")]
    [SerializeField] private float timer;
    [SerializeField] private float timerMulti;
    [SerializeField] private float fishSpeed;
    [SerializeField] private Slider fish;

    [Header("Fishrod")]
    [SerializeField] private float fishrodSpeed;
    [SerializeField] private Scrollbar fishrode;

    public bool isFishing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BaseFishingMach();
        FishMachanic();
        FishrodMachanic();
    }
    void BaseFishingMach()
    {
        if (Input.GetMouseButtonDown(0) && !isFishing)
        {
            isFishing = true;
        }
        else if (Input.GetMouseButtonDown(0) && isFishing)
        {
            isFishing = false;
        }
    }
    void FishMachanic()
    {
        if (!isFishing) return;
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = Random.value * timerMulti;
            fishSpeed = -fishSpeed;
        }
        fish.value += fishSpeed;
    }
    void FishrodMachanic()
    {
        if(!isFishing) return;
        if (Input.GetKey(KeyCode.Space) && fishrode.value < 1)
        {
            fishrode.value += fishrodSpeed;
        }
        else
        {
            fishrode.value += -fishrodSpeed * 1.5f;
        }
    }
}
