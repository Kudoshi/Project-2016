using System;
using System.Collections;
using Kudoshi.Utilities;
using TMPro;
using UI;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnChangeGameState;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Timer gameTimer;
    [SerializeField] private Timer maskTimer;

    [Header("Countdown")] [SerializeField] private GameObject countdownPanel;

    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private float countdownDuration = 3f;

    [Header("End Game")] [SerializeField] private EndGameUI endGameUI;

    [Header("Bonus Time")] [SerializeField]
    private float bonusTimeMax = 5f;

    [SerializeField] private float bonusTimeMin = 0.5f;
    [SerializeField] private float decayRate = 0.15f;
    private bool _isGameActive;

    private int _score;
    public GameState GameState { get; private set; }


    private void OnEnable()
    {
        gameTimer.OnTimerExpired += OnGameTimerExpired;
        maskTimer.OnTimerExpired += OnMaskTimerExpired;
        OnChangeGameState += OnGameStateChanged;
    }

    
    private void OnDisable()
    {
        gameTimer.OnTimerExpired -= OnGameTimerExpired;
        maskTimer.OnTimerExpired -= OnMaskTimerExpired;
        OnChangeGameState -= OnGameStateChanged;

    }

    private void Start()
    {
        _score = 0;
        _isGameActive = true;
        UpdateScoreUI();

        ChangeGameState(GameState.INSTRUCTION);
    }

    public void OnMaskArrived()
    {
        maskTimer.StartTimer();
    }

    public void UpdateMaskSuccess()
    {
        _score++;
        UpdateScoreUI();
        Debug.Log("[GameManager] Mask submitted success");

        // Trigger whatever animations or stuff u need to do
        // Do the increase in timer
        var bonus = bonusTimeMin + (bonusTimeMax - bonusTimeMin) * Mathf.Exp(-decayRate * _score);

        gameTimer.AddTime(bonus);
        maskTimer.StopTimer();
    }

    public void UpdateMaskFail()
    {
        maskTimer.StopTimer();
        Debug.Log("[GameManager] Mask submitted failed");
        // trigger animations or smth
    }

    public void ChangeGameState(GameState gameState)
    {
        GameState = gameState;
        OnChangeGameState?.Invoke(GameState);
    }

    private void OnGameTimerExpired()
    {
        _isGameActive = false;
        maskTimer.StopTimer();
        ShowEndGame();
    }

    private void ShowEndGame()
    {
        ChangeGameState(GameState.ENDGAME);

        if (endGameUI != null)
            endGameUI.Show(_score);
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.GAME)
        {
            gameTimer.StartTimer();
        }
    }
    private void OnMaskTimerExpired()
    {
        if (!_isGameActive) return;

        if (GameState != GameState.GAME) return;

        HydraulicPressDiscard.Instance.Discard();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = _score.ToString();
    }
}

public enum GameState
{
    IDLE, COUNTDOWN, GAME, ENDGAME, INSTRUCTION
}