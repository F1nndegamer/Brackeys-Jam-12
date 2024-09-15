using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaitingForFishUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waitingForFishText;
    [SerializeField] private float animationSpeed = 0.5f; // Speed of the animation in seconds
    private float timer;
    private int currentIteration = 0;
    private const int maxDots = 3; // Maximum number of dots

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = animationSpeed;

            string dots = new string('.', currentIteration);
            waitingForFishText.text = "Waiting For Fish " + dots;

            currentIteration = (currentIteration + 1) % (maxDots + 1);
        }
    }
}
