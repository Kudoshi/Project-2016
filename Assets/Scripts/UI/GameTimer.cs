using TMPro;
using UnityEngine;

namespace UI
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private float totalTime;
        [SerializeField] private TMP_Text timerText;

        public event System.Action OnTimerExpired;

        private float _timeRemaining;
        private bool _isActive;
    
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
        
            int minutes = Mathf.FloorToInt(_timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(_timeRemaining % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}
