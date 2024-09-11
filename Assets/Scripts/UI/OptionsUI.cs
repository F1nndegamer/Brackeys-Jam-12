using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance;
    public static float MusicVolume = 0.4f;
    [SerializeField] private Button backButton;
    [SerializeField] private Slider musicSlider;
    public Slider MusicSlider => musicSlider;
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
            Hide();
        });
        musicSlider.value = MusicVolume;
        Hide();
    }
    public void SaveMusicVolume()
    {
        MusicVolume = musicSlider.value;
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
