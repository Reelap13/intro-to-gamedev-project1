using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndedMenu : MonoBehaviour
{
    [SerializeField] private GameObject _end_game_menu;
    [SerializeField] private GameObject _lose_menu;

    private void Awake()
    {
        _end_game_menu.SetActive(false);
        _lose_menu.SetActive(false);
    }
    public void ShowEndedGameMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        _end_game_menu.SetActive(true);
    }
    public void ShowLoseMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        _lose_menu.SetActive(true);
    }

    public void ComeBackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ContinueGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        _end_game_menu.SetActive(false);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }
}
