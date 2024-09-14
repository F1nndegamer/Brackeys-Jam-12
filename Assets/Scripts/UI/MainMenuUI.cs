using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button howToPlayButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private GameObject tutorialUI;
    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClickSound();
            Loader.Load(Loader.Scene.GameScene);
        });
        optionsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClickSound();
            OptionsUI.Instance.Show(Show);
        });
        howToPlayButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClickSound();
            tutorialUI.SetActive(true);
        });
        creditsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClickSound();
            CreditsUI.Instance.Show(Show);
        });
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
}
