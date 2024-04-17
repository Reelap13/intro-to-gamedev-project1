using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    public class SettingsUI : MonoBehaviour
    {
        [field: SerializeField]
        public SettingsController Controller { get; private set; }

        private void Awake()
        {
            Controller.OnInitializingSettings.AddListener(Initialize);
            Controller.OnSavingSettings.AddListener(Save);
            Controller.OnLoadingSettings.AddListener(Load);
            Controller.OnApplySettings.AddListener(Apply);
        }

        public virtual void Initialize() { }
        public virtual void Save() { }
        public virtual void Load() { }
        public virtual void Apply() { }
    }
}