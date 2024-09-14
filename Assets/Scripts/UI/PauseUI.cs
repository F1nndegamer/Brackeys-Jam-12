using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button backToGameButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button howToPlayButton;
    [SerializeField] private Button backToTitleButton;
    [SerializeField] private GameObject howToPlayUI;
    [SerializeField] private List<FISHDESTROYER> destroyer;
    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        backToGameButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
            SoundManager.Instance.PlayButtonClickSound();
        });
        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show(Show);
            SoundManager.Instance.PlayButtonClickSound();
        });
        howToPlayButton.onClick.AddListener(() =>
        {
            howToPlayUI.SetActive(true);
            SoundManager.Instance.PlayButtonClickSound();
        });
        backToTitleButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
            SoundManager.Instance.PlayButtonClickSound();
        });

        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }
    private void Hide()
    {
        foreach (var destroyer in destroyer)
        {
        destroyer.Go();
        }
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
}
