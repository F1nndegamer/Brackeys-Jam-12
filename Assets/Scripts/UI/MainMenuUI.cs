using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button creditsButton;
    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
            SoundManager.Instance.PlayButtonClickSound();
        });
        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show(Show);
            SoundManager.Instance.PlayButtonClickSound();
        });
        creditsButton.onClick.AddListener(() =>
        {
            CreditsUI.Instance.Show(Show);
            SoundManager.Instance.PlayButtonClickSound();
        });
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
}
