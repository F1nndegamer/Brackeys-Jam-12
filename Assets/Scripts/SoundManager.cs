using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSO audioSO;
    [SerializeField] private AudioSource audioSource;
    private float volume;
    public bool turning;
    public bool stopRepeatingSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume * volumeMultiplier);
    }
    public void PlayButtonClickSound()
    {
        PlaySound(audioSO.buttonClick, Vector3.zero);
    }
    public void PlayFishEngageSound()
    {
        PlaySound(audioSO.fishEngage, Player.Instance.transform.position);
    }
    public void PlayStormSound()
    {
        StartCoroutine(PlayStormRoutine());
    }
    private IEnumerator PlayStormRoutine()
    {
        audioSource.clip = audioSO.StormSounds[0];
        audioSource.Play();

        yield return new WaitWhile(() => audioSource.isPlaying);
        while (!stopRepeatingSound)
        {
            audioSource.clip = audioSO.StormSounds[1];
            audioSource.Play();
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
        audioSource.clip = audioSO.StormSounds[2];
        audioSource.Play();
    }
    public void ChangeVolume()
    {
        volume = OptionsUI.SoundVolume;
        audioSource.volume = volume;
    }
}
