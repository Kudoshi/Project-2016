using UnityEngine;
using Kudoshi.Utilities;
using System.Collections;
using DG.Tweening;

public class PlayerCharacterManager : Singleton<PlayerCharacterManager>
{
    [SerializeField] private SpriteRenderer[] _playerRenderer;

    [Header("Animations")]
    [SerializeField] private Vector2 _randomizeJumpDelay;
    [SerializeField] private Vector2 _randomizeJumpHeight;
    [SerializeField] private Vector2 _randomizeJumpInDuration;
    [SerializeField] private Vector2 _randomizeJumpOutDuration;
    [SerializeField] private float _buttonPressHeight;
    [SerializeField] private float _buttonPressInDuration;
    [SerializeField] private float _buttonPressOutDuration;

    float originalHeight;

    private void Awake()
    {
        originalHeight = _playerRenderer[0].transform.position.y;

    }

    [ContextMenu("Trigger Player Success Animation")]
    public void TriggerPlayerSuccessAnimation()
    {
        foreach(SpriteRenderer player in  _playerRenderer)
        {
            StartCoroutine(JumpPlayers(player));
        }
    }

    [ContextMenu("Trigger Player Press Button Animation")]
    public void Context_TriggerPlayerPressButtonAnimation()
    {
        TriggerPlayerPressButtonAnimation(0);
    }

    public void TriggerPlayerPressButtonAnimation(int playerIndex = 0)
    {
        _playerRenderer[playerIndex].transform.DOKill();
        Vector3 originalPos = _playerRenderer[playerIndex].transform.position;
        originalPos.y = originalHeight;
        _playerRenderer[playerIndex].transform.position = originalPos;

        Sequence seq = DOTween.Sequence();

        seq.Append(_playerRenderer[playerIndex].transform.DOMoveY(_playerRenderer[playerIndex].transform.position.y + _buttonPressHeight, _buttonPressInDuration));

        seq.Append(_playerRenderer[playerIndex].transform.DOMoveY(originalHeight, _buttonPressOutDuration));
    }

    private IEnumerator JumpPlayers(SpriteRenderer player)
    {
        float delayTime = UnityEngine.Random.Range(_randomizeJumpDelay.x, _randomizeJumpDelay.y);
        yield return new WaitForSeconds(delayTime);

        float randomizeHeight = player.transform.position.y + UnityEngine.Random.Range(_randomizeJumpHeight.x, _randomizeJumpHeight.y);
        float randomizeInDuration = UnityEngine.Random.Range(_randomizeJumpInDuration.x, _randomizeJumpInDuration.y);
        float randomizeOutDuration = UnityEngine.Random.Range(_randomizeJumpOutDuration.x, _randomizeJumpOutDuration.y);

        player.transform.DOKill();
        player.transform.DOMoveY(randomizeHeight, randomizeInDuration)
        .OnComplete(() =>
        {
            player.transform.DOMoveY(originalHeight, randomizeOutDuration);
        });
    }
}