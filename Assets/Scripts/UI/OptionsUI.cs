using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance;
    public static float MusicVolume = 0.4f;
    public static float SoundVolume = 0.4f;
    [SerializeField] private Button backButton;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    private Action onCloseButtonAction;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            onCloseButtonAction();
            SoundManager.Instance.PlayButtonClickSound();
            Hide();
        });
        musicSlider.value = MusicVolume;
        soundSlider.value = SoundVolume;
        Hide();
    }
    public void SaveMusicVolume()
    {
        MusicVolume = musicSlider.value;
        SoundManager.Instance.MusicVolume = MusicVolume;

    }
    public void SaveSoundVolume()
    {
        SoundVolume = soundSlider.value;
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);
    }
}
