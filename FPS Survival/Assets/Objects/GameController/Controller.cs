using Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameController
{
    public class Controller : MonoBehaviour
    {
        [field: SerializeField]
        public LevelController LevelController { get; private set; }

        [field: SerializeField] public TimeController TimeController { get; private set; }
        [SerializeField] private int _game_duration = 7;
        [SerializeField] private float _start_day_time = 7 * 60;
        [SerializeField] private float _start_night_time = 21 * 60;
        [SerializeField] private GameEndedMenu _menu;

        public void Initialize()
        {
            TimeController.Initialize();
            TimeController.OnEndingDay.AddListener(EndGame);

            TimeController.AddEvent(new DayEvent(_start_day_time, StopSpawningEnemies));
            TimeController.AddEvent(new DayEvent(_start_night_time, StartSpawningEnemies));
        }

        public void StartGame()
        {
            TimeController.StartTime(_start_day_time);
        }

        public void StartSpawningEnemies()
        {
            Debug.Log("Start spawning");
            LevelController.EnemySystem.StartSpawningEnemies();
        }
        public void StopSpawningEnemies()
        {
            Debug.Log("Stop spawning");
            LevelController.EnemySystem.StopSpawningEnemies();
            LevelController.EnemySystem.KillAllEnemies();
        }
        public void EndGame(int day)
        {
            if (day != _game_duration)
                return;

            _menu.ShowEndedGameMenu();
        }
    }
}