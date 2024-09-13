using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class FishSO : ScriptableObject
{
    public Sprite icon;
    public string fishName;
    [Range(0f, 1f)] public float sweetSpotLength;
    public float pointerSpeed;
    public int requiredCatches;
    public int minPrice;
    public int maxPrice;
}
