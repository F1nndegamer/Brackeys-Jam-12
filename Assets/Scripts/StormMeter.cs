using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormMeter : MonoBehaviour
{
    public StormManager stormManager;
    private float timer;
    void Start()
    {
        timer = stormManager.spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Debug.LogWarning("Storm coming");
            if (stormManager.currentStorm) 
                Debug.Log(Vector2.Distance(stormManager.currentStorm.transform.position, transform.position));
        }
    }
}
