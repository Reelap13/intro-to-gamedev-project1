using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private LevelController _level_controller;
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        _level_controller.GameController.TimeController.OnUpdatingTime.AddListener(UpdateTimer);
    }

    private void UpdateTimer(float time)
    {
        int hours = (int)time / 60;
        int minutes = (int)time % 60;
        _text.text = $"{ParseTimeToTwoDigitNumber(hours)}:{ParseTimeToTwoDigitNumber(minutes)}";
    }

    private string ParseTimeToTwoDigitNumber(int number)
    {
        if (number >= 10)
            return $"{number}";
        else return $"0{number}";
    }
}
