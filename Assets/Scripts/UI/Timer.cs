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
        [SerializeField] private Image sliderFillImage;
        [SerializeField] private AnimationCurve sliderCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        [SerializeField] private Color timerFullColor = Color.green;
        [SerializeField] private Color timerEmptyColor = Color.red;
        [SerializeField] private bool trackElapsedTime;


        [SerializeField] private bool timerTotalTime = false;
        private bool sfxEndGamePlayed = false;

        public event Action OnTimerExpired;

        private float _timeRemaining;
        private float _elapsedTime;
        private bool _isActive;

        public float ElapsedTime => _elapsedTime;

        public void StartTimer()
        {
            _timeRemaining = totalTime;
            if (trackElapsedTime) _elapsedTime = 0f;
            _isActive = true;
        }

        public void StartTimer(float duration)
        {
            _timeRemaining = duration;
            if (trackElapsedTime) _elapsedTime = 0f;
            _isActive = true;
        }
        
        public void AddTime(float seconds)
        {
            _timeRemaining += seconds;
        }

        public void PauseTimer()
        {
            _isActive = false;
        }

        public void ResumeTimer()
        {
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
            if (trackElapsedTime) _elapsedTime += Time.deltaTime;

            if (_timeRemaining <= 0)
            {
                _timeRemaining = 0f;
                _isActive = false;
                OnTimerExpired?.Invoke();
            }

            if (timerTotalTime && !sfxEndGamePlayed && _timeRemaining <= 15)
            {
                sfxEndGamePlayed = true;
                SoundManager.Instance.PlaySound("sfx_timer_warning");
            }
            
            if (timerText != null)
            {
                int minutes = Mathf.FloorToInt(_timeRemaining / 60f);
                int seconds = Mathf.FloorToInt(_timeRemaining % 60f);
                timerText.text = $"{minutes:00}:{seconds:00}";
            }

            if (timerSlider != null)
            {
                float t = Mathf.Clamp01(_timeRemaining / totalTime);
                timerSlider.value = sliderCurve.Evaluate(t);

                if (sliderFillImage != null)
                {
                    sliderFillImage.color = Color.Lerp(timerEmptyColor, timerFullColor, t);
                }
            }
        }
    }
}
