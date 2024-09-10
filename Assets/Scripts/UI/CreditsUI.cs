using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsUI : MonoBehaviour
{
    public static CreditsUI Instance;
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
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            onCloseButtonAction();
        }
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
