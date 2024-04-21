using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [field: SerializeField]
    public MenuController Controller { get; private set; }
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        Controller.OpenSettings();
    }

    public void OpenAuthor()
    {
        Controller.OpenAuthor();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
