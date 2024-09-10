using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI Instance;
    [SerializeField] private TextMeshProUGUI numberOfFishText;
    private Information rangeFinderInformation;
    private void Awake()
    {
        Instance = this;
        rangeFinderInformation = GetComponent<Information>();
    }
    private void Start()
    {
        rangeFinderInformation.enabled = false;
    }
    public void EnableRangeFinder()
    {
        rangeFinderInformation.enabled = true;
    }
}
