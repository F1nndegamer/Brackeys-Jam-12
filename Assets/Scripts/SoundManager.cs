using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSO audioSO;
    private float volume;
    public bool turning;

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
    public void ChangeVolume()
    {
        volume = OptionsUI.SoundVolume;
    }
}
