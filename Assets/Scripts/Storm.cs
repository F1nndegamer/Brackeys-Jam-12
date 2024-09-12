using System.Collections;
using UnityEngine;

public class StormManager : MonoBehaviour
{
    public GameObject stormPrefab; // Storm object prefab
    public Transform player; // Player's transform
    public float spawnInterval = 20f; // Time in seconds between storms
    public float stormSpeed = 5f; // Speed of the storm
    public float outrunDistance = 20f; // Distance the player needs to be ahead of the storm to survive
    public GameObject currentStorm;
    public ParticleSystem particleSystem;
    private float alpha = 0.11f;
    private int x;
    private void Start()
    {
        ;
        StartCoroutine(StormRoutine());
    }

    private IEnumerator StormRoutine()
    {
        var mainModule = particleSystem.main;
        while (true)
        {
            Color color1 = new Color(1f , 1f , 1f, alpha);
            Color color2 = new Color(0.82f , 0.82f , 0.82f, alpha);
            mainModule.startColor = new ParticleSystem.MinMaxGradient(color1,color2);
            alpha += 0.01f;
            yield return new WaitForSeconds(1);
            SpawnStorm();
        }
    }

    void SpawnStorm()
    {
        x++;
        if (x != spawnInterval) return;
        if (currentStorm == null)
        {
            Vector3 spawnPosition = player.position + new Vector3(outrunDistance, 0, -1);
            currentStorm = Instantiate(stormPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void Update()
    {
        if (currentStorm != null)
        {
            // Move storm towards the player
            currentStorm.transform.position = Vector3.MoveTowards(currentStorm.transform.position, player.position, stormSpeed * Time.deltaTime);
            
            // Check if the storm has caught up with the player
            if (Vector3.Distance(currentStorm.transform.position, player.position) < 1f)
            {
                PlayerDeath();
            }

        }
    }
    void PlayerDeath()
    {
        Debug.Log("Player has been caught by the storm! Game over.");
        Destroy(currentStorm);
        alpha = 0.11f;
    }
}
