using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private GameObject optionsUI;
    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
        });
        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show(Show);
        });
        creditsButton.onClick.AddListener(() =>
        {
            CreditsUI.Instance.Show(Show);
        });
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
}
