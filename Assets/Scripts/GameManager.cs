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

    [Header("Mask Timer")]
    [SerializeField] private Vector2 timerCurve;
    [SerializeField] private float difficultyAddition; // Hw much to add per mask completed
    private bool _isGameActive;
    private float difficultyTimer = 0f;

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

        SoundManager.Instance.PlaySound("sfx_test2");
    }

    public void OnMaskArrived()
    {
        float newTime = Mathf.Lerp(timerCurve.x, timerCurve.y, difficultyTimer);
        maskTimer.StartTimer(newTime);
        Debug.Log("[GameManager] New mask timer: " + newTime);
    }

    public void UpdateMaskSuccess()
    {
        _score++;
        UpdateScoreUI();
        Debug.Log("[GameManager] Mask submitted success");

        // Trigger whatever animations or stuff u need to do
        // Do the increase in timer
        var bonus = bonusTimeMin + (bonusTimeMax - bonusTimeMin) * Mathf.Exp(-decayRate * _score);

        difficultyTimer += difficultyAddition;
        gameTimer.AddTime(bonus);
        maskTimer.StopTimer();
    }

    public void UpdateMaskFail()
    {
        maskTimer.StopTimer();
        Debug.Log("[GameManager] Mask submitted failed");
        // trigger animations or smth
    }

    public void PauseMaskTimer()
    {
        maskTimer.PauseTimer();
    }

    public void ResumeMaskTimer()
    {
        maskTimer.ResumeTimer();
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
            endGameUI.Show(_score, gameTimer.ElapsedTime);
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