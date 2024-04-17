using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    public enum DifficultyType
    {
        Easy,
        Normal,
        Hard
    }

    public class Difficulty
    {
        private static int difficultyIndex = 0;
        private static DifficultyType difficultyType = DifficultyType.Easy;

        static Difficulty()
        {
            if (PlayerPrefs.HasKey("Difficulty"))
                difficultyIndex = PlayerPrefs.GetInt("Difficulty");

            ReceiveTypeOfDifficulty();
        }

        public int DifficultyIndex
        {
            set
            {
                difficultyIndex = value;
                PlayerPrefs.SetInt("Difficulty", difficultyIndex);
                ReceiveTypeOfDifficulty();
            }
            get
            {
                return difficultyIndex;
            }
        }

        public DifficultyType DifficultyType
        {
            set
            {
                difficultyType = value;
                ReceiveIndexOfDifficulty();
                PlayerPrefs.SetInt("Difficulty", difficultyIndex);
            }
            get
            {
                return difficultyType;
            }
        }


        static void ReceiveTypeOfDifficulty()
        {
            switch (difficultyIndex)
            {
                case 0:
                    difficultyType = DifficultyType.Easy;
                    break;
                case 1:
                    difficultyType = DifficultyType.Normal;
                    break;
                case 2:
                    difficultyType = DifficultyType.Hard;
                    break;
            }
        }

        static void ReceiveIndexOfDifficulty()
        {
            switch (difficultyType)
            {
                case DifficultyType.Easy:
                    difficultyIndex = 0;
                    break;
                case DifficultyType.Normal:
                    difficultyIndex = 1;
                    break;
                case DifficultyType.Hard:
                    difficultyIndex = 2;
                    break;
            }
        }
    }
}
