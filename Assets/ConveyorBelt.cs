using UnityEngine;
using Kudoshi.Utilities;

public class ConveyorBelt : Singleton<ConveyorBelt>
{
    [SerializeField] private Animator _animator;

    public void StopConveyorBelt()
    {
        _animator.speed = 0;
    }
    
    public void StartConveyorBelt()
    {
        _animator.speed = 1;
    }
}