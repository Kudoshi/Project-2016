using UnityEngine;
using Kudoshi.Utilities;
using DG.Tweening;
using System;

public class HydraulicPressDiscard : Singleton<HydraulicPressDiscard>
{
    [SerializeField] private Transform _hammer;
    [SerializeField] private Transform _hammerDownPosition;
    [SerializeField] private float _hammerHitTime;
    [SerializeField] private float _hammerRetractTime;


    private Vector3 _originalHammerPosition;
    private bool _active = true;

    private void Awake()
    {
        _originalHammerPosition = _hammer.position;
    }

    public void Discard()
    {
        if (MaskController.Instance.FactoryState != FactoryState.MASK_READY) return;

        _active = false;

        SoundManager.Instance.PlaySound("sfx_discard");

        _hammer.DOMove(_hammerDownPosition.position, _hammerHitTime)
       .SetEase(Ease.OutBack)
       .OnComplete(() => {
           HammerBam();

           _hammer.DOMove(_originalHammerPosition, _hammerRetractTime)
            .OnComplete(() => HammerDone());

       });   // overshoot = BOOM

        
    }

    private void HammerBam()
    {
        MaskController.Instance.MaskDiscard();
        CameraController.Instance.TriggerDiscardCamera();
    }

    private void HammerDone()
    {
        // Play PFX;
        MaskController.Instance.SpawnMask();
        ConveyorBelt.Instance.StartConveyorBelt();

        _active = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _active)
        {
            if (GameManager.Instance.GameState == GameState.GAME && MaskController.Instance.FactoryState == FactoryState.MASK_READY)
            {
                Discard();
            }
        }
        
    }
}