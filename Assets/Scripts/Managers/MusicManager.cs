using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    [SerializeField] private AudioClip[] radioTrackArray;
    private AudioSource audioSource;
    private int currentTrack = 0;
    //private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = OptionsUI.MusicVolume;
    }
    private void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayNextGameTrack();
        }
    }
    private void PlayNextGameTrack()
    {
        currentTrack++;
        if (currentTrack >= radioTrackArray.Length)
        {
            currentTrack = 0;
        }
        audioSource.clip = radioTrackArray[currentTrack];
        audioSource.Play();
    }
    public void ChangeVolume()
    {
        audioSource.volume = OptionsUI.MusicVolume;
    }
}
