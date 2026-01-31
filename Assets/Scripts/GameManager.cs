using TMPro;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MaskSpawner maskSpawner;
    [SerializeField] private Timer gameTimer;
    [SerializeField] private Timer maskTimer;
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private float bonusTimeMax = 5f;
    [SerializeField] private float bonusTimeMin = 0.5f;
    [SerializeField] private float decayRate = 0.15f;
    private bool _isGameActive;

    private int _score;

    private void Start()
    {
        _score = 0;
        _isGameActive = true;
        UpdateScoreUI();
        gameTimer.StartTimer();
        maskSpawner.SpawnMask();
    }

    private void OnEnable()
    {
        gameTimer.OnTimerExpired += OnGameTimerExpired;
        maskTimer.OnTimerExpired += OnMaskTimerExpired;
        maskSpawner.OnMaskArrived += OnMaskArrived;
    }

    private void OnDisable()
    {
        gameTimer.OnTimerExpired -= OnGameTimerExpired;
        maskTimer.OnTimerExpired -= OnMaskTimerExpired;
        maskSpawner.OnMaskArrived -= OnMaskArrived;
    }

    public void OnSubmit()
    {
        if (!_isGameActive) return;
        if (maskSpawner.CurrentMask == null) return;

        // TODO: check correctness against target
        maskTimer.StopTimer();

        _score++;
        UpdateScoreUI();

        float bonus = bonusTimeMax * Mathf.Exp(-decayRate * _score);
        
        gameTimer.AddTime(bonus);


        maskSpawner.DespawnMask();

        if (_isGameActive)
            maskSpawner.SpawnMask();
    }

    private void OnMaskArrived(Mask mask)
    {
        // mask is ready for player interaction
        maskTimer.StartTimer();
    }

    private void OnGameTimerExpired()
    {
        _isGameActive = false;
    }

    private void OnMaskTimerExpired()
    {
        if (!_isGameActive) return;
        if (maskSpawner.CurrentMask == null) return;

        maskSpawner.DespawnMask();
        if (_isGameActive)
            maskSpawner.SpawnMask();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = _score.ToString();
    }
}