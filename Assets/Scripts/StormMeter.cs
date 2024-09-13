using Cysharp.Threading.Tasks;
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
        HumidityMeter();
        WindMeter();
        Temperature();
    }

    // Update is called once per frame
    void Update()
    {
        Meters();
    }
    private async void HumidityMeter()
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
            await UniTask.Delay(700);
        }
    }
    private async void WindMeter()
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
            await UniTask.Delay(800);
        }
    }
    private async void Temperature()
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
            await UniTask.Delay(600);
        }
    }
    private async void Meters()
    {
        timer += Time.deltaTime;
        await UniTask.WaitWhile(() => stormManager.Colliding);
        timer = 0;
    }
}
