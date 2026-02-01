
using DG.Tweening;
using Kudoshi.Utilities;
using MilkShake;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] private float _distancePullbackSuccess;
    [SerializeField] private float _durationInPullbackSuccess;
    [SerializeField] private float _durationOutPullbackSuccess;
    [SerializeField] private Camera _camera;

    [Header("Shaker")]
    [SerializeField] private ShakePreset _successShake;
    [SerializeField] private ShakePreset _failShake;
    [SerializeField] private ShakePreset _discardShake;

    private float _originalZAxis;

    private void Awake()
    {
        _originalZAxis = _camera.transform.position.z;
    }

    [ContextMenu("Trigger success camera")]
    public void TriggerSuccessCamera()
    {
        _camera.transform.DOMoveZ(_camera.transform.position.z + _distancePullbackSuccess, _durationInPullbackSuccess)
            .OnComplete(() =>
            {
                _camera.transform.DOMoveZ(_originalZAxis, _durationOutPullbackSuccess);
            });

        Shaker.ShakeAll(_successShake);
    }

    [ContextMenu("Trigger Fail Camera")]
    public void TriggerFailCamera()
    {
        Shaker.ShakeAll(_failShake);
    }

    [ContextMenu("TriggerDiscardCamera")]
    public void TriggerDiscardCamera()
    {
        Shaker.ShakeAll(_discardShake);
    }
}