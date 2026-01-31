using UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MaskSpawner maskSpawner;
    [SerializeField] private Timer timer;
    [SerializeField] private TMP_Text scoreText;

    private int _score;
    private bool _isGameActive;

    private void OnEnable()
    {
        timer.OnTimerExpired += OnTimerExpired;
        maskSpawner.OnMaskArrived += OnMaskArrived;
    }

    private void OnDisable()
    {
        timer.OnTimerExpired -= OnTimerExpired;
        maskSpawner.OnMaskArrived -= OnMaskArrived;
    }

    private void Start()
    {
        _score = 0;
        _isGameActive = true;
        UpdateScoreUI();
        maskSpawner.SpawnMask();
    }

    public void OnSubmit()
    {
        if (!_isGameActive) return;
        if (maskSpawner.CurrentMask == null) return;

        // TODO: check correctness against target
        _score++;
        UpdateScoreUI();

        maskSpawner.DespawnMask();

        if (_isGameActive)
            maskSpawner.SpawnMask();
    }

    private void OnMaskArrived(Mask mask)
    {
        // mask is ready for player interaction
    }

    private void OnTimerExpired()
    {
        _isGameActive = false;
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = _score.ToString();
    }
}