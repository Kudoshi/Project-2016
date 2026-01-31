using System.Collections;
using System;
using UnityEngine;

public class MaskMover : MonoBehaviour
{
    private Coroutine _moveCoroutine;

    public void MoveTo(Vector3 target, float duration, Action onComplete = null)
    {
        if (_moveCoroutine != null) 
            StopCoroutine(_moveCoroutine);
        
        _moveCoroutine = StartCoroutine(MoveRoutine(target, duration, onComplete));
    }

    public void Stop()
    {
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }
    }

    private IEnumerator MoveRoutine(Vector3 target, float duration, Action onComplete)
    {
        Vector3 start = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.position = Vector3.Lerp(start, target, t);

            yield return null;
        }
        
        transform.position = target;
        _moveCoroutine = null;
        onComplete?.Invoke();
    }
}
