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

    [ContextMenu("Trigger Player Success Animation")]
    public void TriggerPlayerSuccessAnimation()
    {
        foreach(SpriteRenderer player in  _playerRenderer)
        {
            StartCoroutine(JumpPlayers(player));
        }
    }

    private IEnumerator JumpPlayers(SpriteRenderer player)
    {
        float delayTime = UnityEngine.Random.Range(_randomizeJumpDelay.x, _randomizeJumpDelay.y);
        yield return new WaitForSeconds(delayTime);

        float randomizeHeight = player.transform.position.y + UnityEngine.Random.Range(_randomizeJumpHeight.x, _randomizeJumpHeight.y);
        float randomizeInDuration = UnityEngine.Random.Range(_randomizeJumpInDuration.x, _randomizeJumpInDuration.y);
        float randomizeOutDuration = UnityEngine.Random.Range(_randomizeJumpOutDuration.x, _randomizeJumpOutDuration.y);

        float originalHeight = player.transform.position.y;
        player.transform.DOMoveY(randomizeHeight, randomizeInDuration)
        .OnComplete(() =>
        {
            player.transform.DOMoveY(originalHeight, randomizeOutDuration);
        });
    }
}