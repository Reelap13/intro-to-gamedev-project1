using System;
using UnityEngine;
using UnityEngine.Events;

public class SettingsController : MonoBehaviour
{
    [field: SerializeField]
    public MenuController Controller { get; private set; }

    [NonSerialized] public UnityEvent OnInitializingSettings = new UnityEvent();
    [NonSerialized] public UnityEvent OnSavingSettings = new UnityEvent();
    [NonSerialized] public UnityEvent OnLoadingSettings = new UnityEvent();
    [NonSerialized] public UnityEvent OnApplySettings = new UnityEvent();

    private void Start()
    {
        InitializeSettings();
        LoadSettings();
        ApplySettings();
    }

    public void InitializeSettings() { OnInitializingSettings.Invoke(); }
    public void SaveSettings() { OnSavingSettings.Invoke(); }
    public void LoadSettings() { OnLoadingSettings.Invoke(); }
    public void ApplySettings() { OnApplySettings.Invoke(); }

    public void ExitFromSettings()
    {
        LoadSettings();
        ApplySettings();
        
        Controller?.OpenMenu();
    }
}
