using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _pause_menu;

    private bool _is_open = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_is_open) CloseMenu();
            else OpenMenu();
        }
    }

    public void OpenMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        _pause_menu.SetActive(true);
        Time.timeScale = 0.0f;
        _is_open = true;
    }
    public void CloseMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _pause_menu.SetActive(false);
        Time.timeScale = 1.0f;
        _is_open = false;
    }
    public void CameBackToMenu()
    {
        CloseMenu();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
}
