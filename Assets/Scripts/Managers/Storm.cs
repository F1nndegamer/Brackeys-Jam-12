using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StormManager : MonoBehaviour
{
    public GameObject stormPrefab; // Storm object prefab
    public Transform player; // Player's transform
    public float spawnInterval = 20f; // Time in seconds between storms
    public float stormSpeed = 5f; // Speed of the storm
    public float outrunDistance = 20f; // Distance the player needs to be ahead of the storm to survive
    public GameObject currentStorm;
    new public ParticleSystem particleSystem; // Particle system for storm visual
    private bool countdownStarted = false; // To prevent multiple countdowns
    private float alpha = 0.11f; // Alpha for particle system color
    private int x = 0; // Counter to track interval
    public bool Colliding;
    public Transform LinePos;
    public static StormManager instance;

    private void Start()
    {
        instance = this;
        StartCoroutine(StormRoutine());
    }

    private IEnumerator StormRoutine()
    {
        var mainModule = particleSystem.main;
        while (true)
        {
            // Gradually increase particle alpha to simulate the storm growing
            Color color1 = new Color(1f, 1f, 1f, alpha);
            Color color2 = new Color(0.82f, 0.82f, 0.82f, alpha);
            mainModule.startColor = new ParticleSystem.MinMaxGradient(color1, color2);
            alpha += 0.01f;
            yield return new WaitForSeconds(1);

            if (currentStorm == null && !SoundManager.Instance.audioSource.isPlaying)
            {
                x++;
                if (x >= spawnInterval)
                {
                    SpawnStorm();
                    x = 0;
                }
            }
        }
    }

    void SpawnStorm()
    {
        if (currentStorm == null)
        {
            SoundManager.Instance.PlayStormSound();
            Camera.main.GetComponent<CameraFollow>().ZoomOut();
            Vector3 spawnPosition = player.position + new Vector3(outrunDistance, 0, -1);
            currentStorm = Instantiate(stormPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void Update()
    {
        if (currentStorm != null)
        {
            SoundManager.Instance.stopRepeatingSound = false;

            // Move storm to the left
            currentStorm.transform.position += Vector3.left * stormSpeed * Time.deltaTime;

            if (currentStorm.transform.position.x < LinePos.position.x + 10) 
            { 
                Destroy(currentStorm); 
                Camera.main.GetComponent<CameraFollow>().ZoomIn();
                SoundManager.Instance.StopStormSound();
            }

            if (Colliding)
            {
                InitiatePlayDeath();
            }
        }
        else
        {
            SoundManager.Instance.stopRepeatingSound = true;

            // Start the countdown if the sound is not playing
            if (!SoundManager.Instance.audioSource.isPlaying && !countdownStarted)
            {
                StartCoroutine(CountdownRoutine());
                countdownStarted = true;
            }

            
        }
        if (currentStorm)
        { 
            if(Vector2.Distance(currentStorm.transform.position, player.position) < currentStorm.transform.localScale.x - 10)
            {
                Colliding = true;
            }
        }
        else
        {
            Colliding = false;
        }
    }

    private IEnumerator CountdownRoutine()
    {
        yield return new WaitForSeconds(5f); // Countdown time of 5 seconds
        Debug.Log("Countdown finished, perform the next action.");
        countdownStarted = false; // Reset flag for future countdowns
    }
    void InitiatePlayDeath()
    {
        Camera.main.GetComponent<CameraFollow>().ZoomIn();
        Destroy(currentStorm);
        GameplayUI.Instance.ResetNumberOfFish();
        GameplayUI.Instance.ShowDeathSequence();
        InventoryUI.Instance.RemoveAllFish();
        SoundManager.Instance.StopStormSound();
        alpha = 0.11f; // Reset alpha for the next storm
        Player.Instance.Die();
    }
}
