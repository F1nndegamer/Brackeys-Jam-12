using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public static GameManager Instance;
    private bool isGamePaused = false;
    public Transform Shore;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        float distance = Player.Instance.transform.position.x - Shore.position.x;
        if (distance < 0)
        {
            Player.Instance.isNearShop = true;
        }
        else
        {
            Player.Instance.isNearShop = false;
        }
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseGame();
        }
    }
    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }
}
