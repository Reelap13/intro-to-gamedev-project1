using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Loader : MonoBehaviour
{
    [SerializeField] AudioMixerGroup mixer;
    private void Start()//с Awake не работает почему-то
    {
        LoadSetting();
    }

    void LoadSetting()
    {
        Screen.SetResolution(Setting.Screen.Resolution.width, Setting.Screen.Resolution.height, Setting.Screen.IsFullScreen);
        QualitySettings.SetQualityLevel(Setting.Quality.QualityIndex);

        mixer.audioMixer.SetFloat("MusicVolume", Setting.Audio.MusicVolume);
        mixer.audioMixer.SetFloat("SoundVolume", Setting.Audio.SoundVolume);
    }
}
