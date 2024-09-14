using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class FishSO : ScriptableObject
{
    public Sprite icon;
    public string fishName;
    public string description;
    public float weight;
    public float length;
    [Range(0f, 1f)] public float sweetSpotLength;
    public float pointerSpeed;
    public int requiredCatches;
    public int minPrice;
    public int maxPrice;
}
