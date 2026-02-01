using UnityEngine;
using Kudoshi.Utilities;
using System;
using DG.Tweening;

public class InstructionController : Singleton<InstructionController>
{
    [SerializeField] private GameObject _container;

    [Header("Animation")]
    [SerializeField] private float _popinDuration;
    [SerializeField] private float _popoutDuration;

    private bool _isActive = false;

    private void Awake()
    {
        _container.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.OnChangeGameState += OnChangeGameState;

    }

    private void OnDisable()
    {
        GameManager.OnChangeGameState -= OnChangeGameState;

    }
    private void OnChangeGameState(GameState state)
    {
        if (state == GameState.INSTRUCTION)
        {
            TriggerInstructionBoard();
        }
    }

    private void TriggerInstructionBoard()
    {
        _isActive = true;

        Vector3 originalScale = _container.transform.localScale;

        SoundManager.Instance.PlaySound("sfx_popin");

        _container.transform.localScale = Vector3.zero;
        _container.transform.DOScale(originalScale, _popinDuration).SetEase(Ease.OutElastic);
        _container.gameObject.SetActive(true);
    }

    private void EndInstructionBoard()
    {
        _isActive = false;

        SoundManager.Instance.PlaySound("sfx_popout");
        _container.transform.DOScale(Vector3.zero, _popoutDuration)
            .SetEase(Ease.InBack)
        .OnComplete(() =>
        {
            _container.gameObject.SetActive(false);
            Util.WaitForSeconds(this, () => GameManager.Instance.ChangeGameState(GameState.COUNTDOWN), 0.25f);
            
        });
        
    }

    private void Update()
    {
        if (!_isActive) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndInstructionBoard();
        }
    }
}