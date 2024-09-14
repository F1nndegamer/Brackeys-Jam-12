using System.Collections;
using UnityEditor;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSO audioSO;
    public float MusicVolume = 5f;
    public AudioSource audioSource;
    public AudioSource Buttonaudio;
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
        Buttonaudio.clip = audioSO.buttonClick[Random.Range(0, audioSO.buttonClick.Length)];
        Buttonaudio.Play();
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
    public IEnumerator PlayStormRoutine()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in allAudioSources)
        {
            if (source != audioSource)
            {
                source.Pause();
            }
        }

        // Play the initial storm sound
        audioSource.clip = audioSO.StormSounds[0];
        audioSource.Play();
        if(stopRepeatingSound)
        {
        yield return StartCoroutine(FadeOut(audioSource, 2f));
        }
        yield return new WaitWhile(() => audioSource.isPlaying);

        // Loop the repeating storm sound until `stopRepeatingSound` becomes true
        while (!stopRepeatingSound)
        {
            audioSource.clip = audioSO.StormSounds[1];
            audioSource.Play();
            yield return new WaitWhile(() => audioSource.isPlaying);
        }

        // Start fading out the sound
        yield return StartCoroutine(FadeOut(audioSource, 2f));
        // Resume all paused audio sources
        foreach (AudioSource source in allAudioSources)
        {
            if (source != audioSource)
            {
                source.UnPause();
            }
        }
    }

    private IEnumerator FadeOut(AudioSource source, float fadeDuration)
    {
        float startVolume = source.volume;

        // Gradually reduce volume over the fadeDuration
        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        // Ensure the volume is set to 0 and stop the audio
        source.volume = 0;
        source.Stop();
    }
    private void Update()
    {
        if (stopRepeatingSound)
        {
            stopRepeatingSound = false;
        }
        else
        {

            if (audioSource == null) return;
            audioSource.volume = MusicVolume;
        }
    }

    public void ChangeVolume()
    {
        volume = OptionsUI.SoundVolume;
    }
}
