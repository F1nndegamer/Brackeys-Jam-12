using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class StormMeter : MonoBehaviour
{
    public StormManager stormManager;
    [SerializeField] private TMP_Text humidityMeter;
    [SerializeField] private TMP_Text windMeter;
    [SerializeField] private TMP_Text temperature;
    private float timer;
    void Start()
    {
        timer = 0;
        StartCoroutine(MetersCoroutine());
        StartCoroutine(HumidityMeter());
        StartCoroutine(WindMeter());
        StartCoroutine(Temperature());
    }
    private IEnumerator HumidityMeter()
    {
        while (true)
        {
            if (timer > stormManager.spawnInterval - 2)
            {
                humidityMeter.text = "%" + ((int)Random.Range(70, 90)).ToString();
            }
            else
            {
                humidityMeter.text = "%" + ((int)Random.Range(timer + 10, timer + 40)).ToString();
            }
            yield return new WaitForSeconds(0.8f);
        }
    }
    private IEnumerator WindMeter()
    {
        while (true)
        {
            if (timer > stormManager.spawnInterval - 2)
            {
                windMeter.text = Random.Range(60, 80).ToString("F1");
            }
            else
            {
                windMeter.text = Random.Range(timer +  10, timer + 30).ToString("F1");
            }
            yield return new WaitForSeconds(0.8f);
        }
    }
    private IEnumerator Temperature()
    {
        while (true)
        {
            if (timer > stormManager.spawnInterval - 2)
            {
                temperature.text = Random.Range(-5, 10).ToString("F1") + "C°";
            }
            else
            {
                temperature.text = Random.Range(25, 35).ToString("F1") + "C°";
            }
            yield return new WaitForSeconds(0.6f);
        }
    }
    private IEnumerator MetersCoroutine()
    {
        while (true)
        {
            timer += Time.deltaTime;

            // Wait until the storm manager is not colliding
            yield return new WaitWhile(() => stormManager.Colliding);

            timer = 0f;
        }
    }
}
