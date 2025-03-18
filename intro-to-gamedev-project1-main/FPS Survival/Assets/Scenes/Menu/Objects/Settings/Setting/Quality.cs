using UnityEngine;

namespace Settings
{
    public class Quality
    {
        private static int qualityIndex = 0;
        static Quality()
        {
            if (PlayerPrefs.HasKey("QualityIndex"))
                qualityIndex = PlayerPrefs.GetInt("QualityIndex");
        }

        public int QualityIndex
        {
            set
            {
                qualityIndex = value;
                PlayerPrefs.SetInt("QualityIndex", qualityIndex);
            }
            get
            {
                return qualityIndex;
            }
        }
    }
}