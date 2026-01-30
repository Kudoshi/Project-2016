using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float totalTime;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private Slider timerSlider;

        public event System.Action OnTimerExpired;

        private float _timeRemaining;
        private bool _isActive;

        private void Start()
        {
            StartTimer();
        }

        public void StartTimer()
        {
            _timeRemaining = totalTime;
            _isActive = true;
        }
    
        public void StartTimer(float duration)
        {
            _timeRemaining = duration;
            _isActive = true;
        }

        public void StopTimer()
        {
            _isActive = false;
        }

        private void Update()
        {
            if (!_isActive) return;
        
            _timeRemaining -= Time.deltaTime;

            if (_timeRemaining <= 0)
            {
                _timeRemaining = 0f;
                _isActive = false;
                OnTimerExpired?.Invoke();
            }


            if (timerText != null)
            {
                int minutes = Mathf.FloorToInt(_timeRemaining / 60f);
                int seconds = Mathf.FloorToInt(_timeRemaining % 60f);
                timerText.text = $"{minutes:00}:{seconds:00}";
            }

            if (timerSlider != null)
            {
                timerSlider.value = _timeRemaining / totalTime;
            }

        }
    }
}
