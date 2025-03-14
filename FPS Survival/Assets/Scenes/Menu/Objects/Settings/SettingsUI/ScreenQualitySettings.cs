using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class ScreenQualitySettings : SettingsUI
    {
        [SerializeField] private TMP_Dropdown _quality_dropdown;

        public override void Initialize()
        {
            _quality_dropdown.onValueChanged.AddListener(SetQuality);
            CompilationOfQualityDropdown();
        }
        public override void Save()
        {
            Setting.Quality.QualityIndex = _quality_dropdown.value;
        }
        public override void Load()
        {
            _quality_dropdown.value = Setting.Quality.QualityIndex;
        }
        public override void Apply()
        {
            SetQuality(_quality_dropdown.value);
        }

        private void SetQuality(int quality_index)
        {
            QualitySettings.SetQualityLevel(quality_index);
        }

        private void CompilationOfQualityDropdown()
        {
            _quality_dropdown.ClearOptions();
            //List<string> options = new List<string>();
            string[] qualityLevels = QualitySettings.names;
            _quality_dropdown.AddOptions(new List<string>(qualityLevels));
        }
    }
}

