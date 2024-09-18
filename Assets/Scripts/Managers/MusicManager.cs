using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    [SerializeField] private AudioClip[] radioTrackArray;
    public AudioSource audioSource;
    private int currentTrack = 0;
    private double lengthofMusic;
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
        if (audioSource.clip != null && audioSource.clip.frequency > 0)
        {
            lengthofMusic = (double)(audioSource.clip.samples / audioSource.clip.frequency);
        }
        else
        {
            Debug.LogWarning("Audio clip is null or has an invalid frequency.");
            lengthofMusic = 0;  // Fallback or handle as needed
        }

    }
    private void Update()
    {
        lengthofMusic -= Time.deltaTime;
        if (lengthofMusic <= 0)
        {
            PlayNextGameTrack();
        }
    }
    public void PlayNextGameTrack()
    {
        currentTrack++;
        if (currentTrack >= radioTrackArray.Length)
        {
            currentTrack = 0;
        }
        StartCoroutine(Fades());
    }
    public void ChangeVolume()
    {
        audioSource.volume = OptionsUI.MusicVolume;
    }
    public void PlayFadeOut(int duration = 2)
    {
        StartCoroutine(FadeOut(duration));
    }
    public void PlayFadeIn(int duration = 2)
    {
        StartCoroutine(FadeIn(duration));
    }

    IEnumerator FadeIn(int duration)
    {
        float timer = 0;
        float startVol = audioSource.volume;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVol, OptionsUI.MusicVolume, timer  / duration);
            yield return null;
        }
        yield break;
    }
    IEnumerator FadeOut(int duration)
    {
        float timer = 0;
        float startVol = audioSource.volume;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVol, 0, timer / duration);
            yield return null;
        }
        yield break;
    }
    IEnumerator Fades(int duration = 2)
    {
        PlayFadeOut(duration);
        yield return new WaitForSeconds(duration);
        audioSource.clip = radioTrackArray[currentTrack];
        audioSource.Play();
        PlayFadeIn(duration);
    }
}
