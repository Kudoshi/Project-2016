
using DG.Tweening;
using UnityEngine;

public class PlayerAnimationSqueezeAnimation : MonoBehaviour
{
    [SerializeField] private float _squeezeAmt;
    [SerializeField] private float _squeezeDuration;
    private void Start()
    {
        transform.DOScaleY(_squeezeDuration, _squeezeDuration)
          .SetEase(Ease.InOutSine)
          .SetLoops(-1, LoopType.Yoyo);
    }
}