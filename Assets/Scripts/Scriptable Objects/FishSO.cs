using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class FishSO : ScriptableObject
{
    public Sprite icon;
    public string fishName;
    [Range(0f, 1f)] public float greenBarLong;
    public float whiteBarSpeed;
    public float depth;
    public int price;
}
