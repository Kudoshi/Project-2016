using UnityEngine;
using Kudoshi.Utilities;

public class ConveyorBelt : Singleton<ConveyorBelt>
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _conveyorAudioSource;

    public void StopConveyorBelt()
    {
        _animator.speed = 0;
        _conveyorAudioSource.Pause();
        SoundManager.Instance.PlaySound("sfx_conveyor_stop");
    }

    public void StartConveyorBelt()
    {
        _animator.speed = 1;
        _conveyorAudioSource.Play();
        SoundManager.Instance.PlaySound("sfx_conveyor_start");
    }
}