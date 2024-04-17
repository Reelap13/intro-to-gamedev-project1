using UnityEngine;
using UnityEngine.Rendering;

namespace Settings
{
    public class Audio
    {
        private static float musicVolume = 0;
        private static float soundVolume = 0;
        private static float voiceoverVolume = 0;

        static Audio()
        {
            if (PlayerPrefs.HasKey("MusicVolume"))
                musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            if (PlayerPrefs.HasKey("SoundVolume"))
                soundVolume = PlayerPrefs.GetFloat("SoundVolume");
            if (PlayerPrefs.HasKey("VoiceoverVolume"))
                soundVolume = PlayerPrefs.GetFloat("VoiceoverVolume");
        }

        public float MusicVolume
        {
            set
            {
                musicVolume = value;
                PlayerPrefs.SetFloat("MusicVolume", musicVolume);
            }
            get
            {
                return musicVolume;
            }
        }

        public float SoundVolume
        {
            set
            {
                soundVolume = value;
                PlayerPrefs.SetFloat("SoundVolume", soundVolume);
            }
            get
            {
                return soundVolume;
            }
        }

        public float VoiceoverVolume
        {
            set
            {
                voiceoverVolume = value;
                PlayerPrefs.SetFloat("VoiceoverVolume", voiceoverVolume);
            }
            get
            {
                return voiceoverVolume;
            }
        }

        public void SetVolume(string name, float volume)
        {
            UnityEngine.Debug.Log(name);
            UnityEngine.Debug.Log($"{MusicVolume} {SoundVolume} {VoiceoverVolume}");
            switch (name)
            {
                case "MusicsVolume":
                    MusicVolume = volume;
                    break;
                case "SoundsVolume":
                    SoundVolume = volume;
                    break;
                case "VoiceoversVolume":
                    VoiceoverVolume = volume;
                    break;
            }

            UnityEngine.Debug.Log($"{MusicVolume} {SoundVolume} {VoiceoverVolume}");
        }

        public float GetVolume(string name)
        {
            switch (name)
            {
                case "MusicsVolume":
                    return MusicVolume;
                case "SoundsVolume":
                    return SoundVolume;
                case "VoiceoversVolume":
                    return VoiceoverVolume;
                default:
                    return 0f;
            }
        }
    }
}