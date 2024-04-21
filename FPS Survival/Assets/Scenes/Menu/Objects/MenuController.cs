using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _main_menu;
    [SerializeField] private GameObject _settings_menu;
    [SerializeField] private GameObject _author_menu;

    private void Awake()
    {
        OpenMenu();
    }
    public void OpenSettings()
    {
        _main_menu.SetActive(false);
        _settings_menu.SetActive(true);
        _author_menu.SetActive(false);
    }
    public void OpenSaves()
    {
        _main_menu.SetActive(false);
        _settings_menu.SetActive(false);
        _author_menu.SetActive(false);
    }
    public void OpenAuthor()
    {
        _main_menu.SetActive(false);
        _settings_menu.SetActive(false);
        _author_menu.SetActive(true);
    }

    public void OpenMenu()
    {
        _main_menu.SetActive(true);
        _settings_menu.SetActive(false);
        _author_menu.SetActive(false);
    }
}
