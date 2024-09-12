using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSO audioSO;
    [SerializeField] private AudioSource audioSource; // Dedicated AudioSource for storm sounds
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
        // Pause all other audio sources in the scene
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in allAudioSources)
        {
            if (source != audioSource)
            {
                source.Pause();
            }
        }

        // Play the first storm sound (e.g., thunder)
        audioSource.clip = audioSO.StormSounds[0];
        audioSource.Play();
        yield return new WaitWhile(() => audioSource.isPlaying);

        // Repeat the second storm sound (e.g., wind) until stopRepeatingSound is true
        while (!stopRepeatingSound)
        {
            audioSource.clip = audioSO.StormSounds[1];
            audioSource.Play();
            yield return new WaitWhile(() => audioSource.isPlaying);

            // Break out immediately if stopRepeatingSound becomes true
            if (stopRepeatingSound)
            {
                break;
            }
        }

        // Play the final storm sound immediately after the loop is broken
        audioSource.clip = audioSO.StormSounds[2]; // Final storm sound (e.g., storm fading away)
        audioSource.Play();

        // Wait until the final sound finishes playing
        yield return new WaitWhile(() => audioSource.isPlaying);

        // Resume all paused audio sources after the storm ends
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
        // Trigger immediate outro if stopRepeatingSound becomes true
        if (stopRepeatingSound)
        {
            // Stop the repeating sound and transition to the outro
            stopRepeatingSound = false; // Reset to prevent retriggering
        }
    }

    public void ChangeVolume()
    {
        volume = OptionsUI.SoundVolume;
        audioSource.volume = volume;
    }
}
