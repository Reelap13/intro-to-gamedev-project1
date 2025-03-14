using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class FullScreenToggle : SettingsUI
    {
        [SerializeField] private Toggle _fullScreen_toggle;

        public override void Initialize()
        {
            _fullScreen_toggle.onValueChanged.AddListener(SetFullScreen);
        }
        public override void Save()
        {
            Setting.Screen.IsFullScreen = _fullScreen_toggle.isOn;
        }
        public override void Load()
        {
            _fullScreen_toggle.isOn = Setting.Screen.IsFullScreen;
        }
        public override void Apply()
        {
            SetFullScreen(_fullScreen_toggle.isOn);
        }

        public void SetFullScreen(bool is_full_screen)
        {
            Screen.fullScreen = is_full_screen;
        }
    }
}
