using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormMeter : MonoBehaviour
{
    public StormManager stormManager;
    public Animator animator;
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
            animator.SetBool("waring", true);
            if (stormManager.currentStorm)
            {
                Debug.Log(Vector2.Distance(stormManager.currentStorm.transform.position, transform.position));
                animator.speed = 10 / Vector2.Distance(stormManager.currentStorm.transform.position, transform.position);
            }
        }
    }
}
