using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndedMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;

    private void Awake()
    {
        _menu.SetActive(false);
    }
    public void ShowEndedGameMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        _menu.SetActive(true);
    }

    public void ComeBackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ContinueGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        _menu.SetActive(false);
    }
}
