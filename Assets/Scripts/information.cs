using UnityEngine;
using TMPro;

public class Information : MonoBehaviour
{
    public Transform Player;
    public Transform Shore;
    public TMP_Text DistanceText;

    // Update is called once per frame
    void Update()
    {
        float distance = Mathf.Abs(Player.position.x - Shore.position.x);
        DistanceText.text = $"Distance to shore = {distance:F0}M";
    }
}
