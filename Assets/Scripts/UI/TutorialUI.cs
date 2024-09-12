using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private Button backButton;
    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClickSound();
            Hide();
        });
        Hide();
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
