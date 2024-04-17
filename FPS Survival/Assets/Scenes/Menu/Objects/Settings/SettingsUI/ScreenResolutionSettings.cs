using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Settings
{
    public class ScreenResolutionSettings : SettingsUI
    {
        [SerializeField] private TMP_Dropdown _resolution_dropdown;
        private Resolution[] _resolutions;

        public override void Initialize()
        {
            _resolution_dropdown.onValueChanged.AddListener(SetResolution);
            CompilationOfResolutionDropdown();
        }
        public override void Save()
        {
            Setting.Screen.ResolutionIndex = _resolution_dropdown.value;
        }
        public override void Load()
        {
            _resolution_dropdown.value = Setting.Screen.ResolutionIndex;
        }
        public override void Apply()
        {
            SetResolution(_resolution_dropdown.value);
        }

        private void SetResolution(int resolution_index)
        {
            Resolution resolution = _resolutions[resolution_index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
        private void CompilationOfResolutionDropdown()
        {
            _resolution_dropdown.ClearOptions();

            List<string> options = new List<string>();
            _resolutions = Screen.resolutions;
            int currentResolutionIndex = 0;

            for (int i = 0; i < _resolutions.Length; ++i)
            {
                string option = _resolutions[i].width + "x" + _resolutions[i].height + " " + _resolutions[i].refreshRateRatio + "Hz";
                options.Add(option);
                if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            _resolution_dropdown.AddOptions(options);
            _resolution_dropdown.RefreshShownValue();
        }

    }
}