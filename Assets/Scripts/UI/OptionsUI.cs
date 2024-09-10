using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance;
    [SerializeField] private Button backButton;
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
        Hide();
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
