
using DG.Tweening;
using System;
using UnityEngine;

public class PlayerAnimationSqueezeAnimation : MonoBehaviour
{
    [SerializeField] private float _squeezeAmt;
    [SerializeField] private float _squeezeDuration;


    private void Start()
    {
        Util.WaitForSeconds(this, () =>
        {
            transform.DOScaleY(transform.localScale.y * _squeezeAmt, _squeezeDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        }, UnityEngine.Random.Range(0, 2));
    }
}