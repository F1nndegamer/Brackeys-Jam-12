using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSO audioSO;
    public AudioSource audioSource;
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
        volume = OptionsUI.SoundVolume;
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
        PlaySound(audioSO.buttonClick, Camera.main.transform.position);
    }

    public void PlayFishEngageSound()
    {
        PlaySound(audioSO.fishEngage, Player.Instance.transform.position, 6f);
    }

    public void PlayStormSound()
    {
        StartCoroutine(PlayStormRoutine());
    }
    public void PlayFishEscapeSound()
    {
        PlaySound(audioSO.fishEscape, Player.Instance.transform.position, 8f);
    }
    public void PlayThrowRodSound()
    {
        PlaySound(audioSO.castRod, Player.Instance.transform.position, 8f);
    }
    public void PlayCatchFishSound(int index)
    {
        PlaySound(audioSO.catchFish[index], Player.Instance.transform.position, 8f);
    }
    public void PlaySpendMoneySound()
    {
        PlaySound(audioSO.spendMoney, Vector3.zero, 8f);
    }
    private IEnumerator PlayStormRoutine()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in allAudioSources)
        {
            if (source != audioSource)
            {
                source.Pause();
            }
        }

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
        yield return new WaitWhile(() => audioSource.isPlaying);
        foreach (AudioSource source in allAudioSources)
        {
            if (source != audioSource)
            {
                source.UnPause();
            }
        }
    }

    private void Update()
    {
        if (stopRepeatingSound)
        {
            stopRepeatingSound = false;
        }
    }

    public void ChangeVolume()
    {
        volume = OptionsUI.SoundVolume;
        audioSource.volume = volume;
    }
}
