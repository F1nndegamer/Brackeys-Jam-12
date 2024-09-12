using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class AudioSO : ScriptableObject
{
    public AudioClip[] buttonClick;
    public AudioClip fishEngage;
    public AudioClip fishEscape;
    public List<AudioClip> StormSounds;
    public AudioClip castRod;
    public AudioClip[] catchFish;
}
