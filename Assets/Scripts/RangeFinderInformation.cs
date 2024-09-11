using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RangeFinderInformation : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform shore;
    [SerializeField] private TMP_Text distanceText;
    public static float distance;
    private void Start()
    {
        player = Player.Instance.transform;
    }
    // Update is called once per frame
    void Update()
    {
        distance = Mathf.Abs(player.position.x - shore.position.x);
        distanceText.text = $"Distance to shore = {distance:F0}M";
    }
}
