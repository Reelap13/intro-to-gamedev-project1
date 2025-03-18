using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Settings
{
    public class AudioValueSettings : SettingsUI
    {
        [SerializeField] private AudioMixerGroup _mixer;
        [SerializeField] private Slider _slider;
        [SerializeField] private string _group_name;
        public override void Initialize()
        {
            _slider.onValueChanged.AddListener(SetVolume);
        }
        public override void Save()
        {
            //_mixer.audioMixer.GetFloat(_group_name, out float audioValue);
            float audioValue = _slider.value;
            Debug.Log(_group_name + ": " + audioValue);
            Setting.Audio.SetVolume(_group_name, audioValue);
        }

        public override void Load()
        {
            _slider.value = Setting.Audio.GetVolume(_group_name);
        }

        public override void Apply()
        {
            SetVolume(_slider.value);
        }

        public void SetVolume(float volume)
        {
            Debug.Log(_group_name + ": " + volume);
            if (volume < 0f) volume *= 4;
            Debug.Log(_group_name+ ": " + volume);
            _mixer.audioMixer.SetFloat(_group_name, volume);
        }
    }
}
