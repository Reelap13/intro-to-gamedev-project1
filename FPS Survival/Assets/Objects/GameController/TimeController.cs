using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace GameController
{
    public delegate void CompletedFunction();
    public class TimeController : MonoBehaviour
    {
        [NonSerialized] public UnityEvent<int> OnStartingDay = new UnityEvent<int>();
        [NonSerialized] public UnityEvent<int> OnEndingDay = new UnityEvent<int>();
        [NonSerialized] public UnityEvent<float> OnUpdatingTime = new UnityEvent<float>();

        [SerializeField] private Transform _sun;
        [SerializeField] private float _time_speed = 100f;
        [SerializeField] private Vector3 _sun_offset = new Vector3(-235f, 30f, 0);

        private const float DAY_DURATION = 60 * 24;

        private int _day_number;
        private float _time;
        private HashSet<DayEvent> _events = new HashSet<DayEvent>();
        private HashSet<DayEvent> _day_events;

        public void Initialize()
        {
            _day_number = 0;
            _time = 0;
        }

        public void StartTime(float time)
        {
            StartNextDay();
            _time = time;
        }

        private void Update()
        {
            _time += Time.deltaTime * _time_speed;
            UpdateDay();
            UpdateEvenets();
            RotateSun();

            OnUpdatingTime.Invoke(_time);
        }

        private void UpdateEvenets()
        {
            List<DayEvent> completed_events = new List<DayEvent>();
            foreach (var _day_event in _day_events)
            {
                if (_day_event.Time > _time)
                    continue;

                _day_event.Complete();
                completed_events.Add(_day_event);
            }

            foreach (var _day_event in completed_events)
                _day_events.Remove(_day_event);
        }

        private void UpdateDay()
        {
            if (_time < DAY_DURATION)
                return;
            StartNextDay();
        }

        private void RotateSun()
        {

            Vector3 sun_rotation = new Vector3(_time * 360 / DAY_DURATION, 0, 0) + _sun_offset;
            Debug.Log(sun_rotation);
            _sun.transform.eulerAngles = sun_rotation;
            //_sun.transform.rotation = Quaternion.Euler(sun_rotation);
        }

        private void StartNextDay()
        {
            OnEndingDay.Invoke(_day_number);
            _time = 0;
            ++_day_number;
            SetDayEvenets();
            OnStartingDay.Invoke(_day_number);
        }

        private void SetDayEvenets()
        {
            _day_events = new HashSet<DayEvent>();
            foreach (var day_event in _events)
                _day_events.Add(day_event);
        }

        public void AddEvent(DayEvent day_event) => _events.Add(day_event);
    }

    public class DayEvent
    {
        public float Time;
        public CompletedFunction Complete;

        public DayEvent(float time, CompletedFunction complete)
        {
            Time = time;
            Complete = complete;
        }
    }
}