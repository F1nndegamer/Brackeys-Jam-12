using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        timer += Time.deltaTime;
    }
    private async void HumidityMeter()
    {
        while (true)
        {
            await UniTask.Delay(700);
            if (timer > stormManager.spawnInterval - 2)
            {
                humidityMeter.text = "%" + ((int)Random.Range(80, 90));
            }
            else
            {
                humidityMeter.text = "%" + ((int)Random.Range(timer + 5, timer + 30));
            }
        }
    }
    private async void WindMeter()
    {
        while (true)
        {
            await UniTask.Delay(600);
            if (timer > stormManager.spawnInterval - 2)
            {
                windMeter.text = Random.Range(60, 80).ToString("F1");
            }
            else
            {
                windMeter.text = Random.Range(timer, timer + 10).ToString("F1");
            }
        }
    }
    private async void Temperature()
    {
        while (true)
        {
            await UniTask.Delay(800);
            if (timer > stormManager.spawnInterval - 2)
            {
                temperature.text = Random.Range(10, 25).ToString("F1") + "C°";
            }
            else
            {
                temperature.text = Random.Range(60, 80).ToString("F1") + "C°";
            }
        }
    }
}
