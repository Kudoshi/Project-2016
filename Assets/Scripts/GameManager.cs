using TMPro;
using UI;
using UnityEngine;
using Kudoshi.Utilities;
using System;

public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnChangeGameState;

    [SerializeField] private Timer timer;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private MaskSpawner maskSpawner;
    [SerializeField] private Timer gameTimer;
    [SerializeField] private Timer maskTimer;
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private float bonusTimeMax = 5f;
    [SerializeField] private float bonusTimeMin = 0.5f;
    [SerializeField] private float decayRate = 0.15f;
    private GameState _gameState;

    private int _score;
    private bool _isGameActive;
    public GameState GameState { get => _gameState;}

    private void OnEnable()
    {
        timer.OnTimerExpired += OnTimerExpired;
    }

    private void OnDisable()
    {
        timer.OnTimerExpired -= OnTimerExpired;
    }
   
    private void Start()
    {
        _score = 0;
        _isGameActive = true;
        UpdateScoreUI();

        ChangeGameState(GameState.GAME);
    }

    public void UpdateMaskSuccess()
    {
        _score++;
        UpdateScoreUI();
        Debug.Log("[GameManager] Mask submitted success");

        // Trigger whatever animations or stuff u need to do
        // Do the increase in timer
        float bonus = bonusTimeMax * Mathf.Exp(-decayRate * _score);
        
        gameTimer.AddTime(bonus);
    }

    public void UpdateMaskFail()
    {
        Debug.Log("[GameManager] Mask submitted failed");
        // trigger animations or smth
    }


    public void ChangeGameState(GameState gameState)
    {
        _gameState = gameState;
        OnChangeGameState?.Invoke(_gameState);
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

public enum GameState
{
    IDLE, COUNTDOWN, GAME, ENDGAME
}