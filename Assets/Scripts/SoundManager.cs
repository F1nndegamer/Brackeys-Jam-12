using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSO audioSO;
    [SerializeField] private float volume = 1f;
    public bool turning;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource audioSource = new GameObject("Sound").AddComponent<AudioSource>();
        audioSource.transform.position = position;
        audioSource.clip = audioClip;
        audioSource.volume = volume * volumeMultiplier;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioClip.length);
    }
    public void PlayMenuSFX()
    {
        PlaySound(audioSO.MenuSFX, Vector3.zero);
    } 
}
