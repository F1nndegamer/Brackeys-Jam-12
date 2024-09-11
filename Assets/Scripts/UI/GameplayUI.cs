using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private FishingMachanic fishingMechanic;
    public static GameplayUI Instance;
    [SerializeField] private TextMeshProUGUI numberOfFishText;
    private RangeFinderInformation rangeFinderInformation;
    [SerializeField] private RawImage warning;
    private void Awake()
    {
        Instance = this;
        rangeFinderInformation = GetComponent<RangeFinderInformation>();
    }
    private void Start()
    {
        fishingMechanic.OnFishCaught += FishingMechanic_OnFishCaught;
        rangeFinderInformation.enabled = false;
        warning.enabled = false;
    }

    private void FishingMechanic_OnFishCaught(object sender, System.EventArgs e)
    {
        numberOfFishText.text = FishingMachanic.basket.Sum(x => x.Value).ToString();
    }

    public void EnableRangeFinder()
    {
        rangeFinderInformation.enabled = true;
    }
    public void MultiPurposeMeter()
    {
        warning.enabled = true;
    }
}
