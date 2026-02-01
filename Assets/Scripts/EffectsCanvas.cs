using UnityEngine;
using Kudoshi.Utilities;
using DG.Tweening;

public class EffectsCanvas : Singleton<EffectsCanvas>
{
    [SerializeField] private GameObject _successIndicator;
    [SerializeField] private GameObject _failIndicator;
    [SerializeField] private float _indicatorPopinTime;
    [SerializeField] private float _indicatorPauseAppearTime;
    [SerializeField] private float _indicatorPopoutTime;

    private void Awake()
    {
        _successIndicator.gameObject.SetActive(false);  
        _failIndicator.gameObject.SetActive(false);
    }

    [ContextMenu("Trigger Success")]
    public void TriggerSuccess()
    {
        AnimateIndicator(_successIndicator);   
    }

    [ContextMenu("Trigger Failure")]
    public void TriggerFailure()
    {
        AnimateIndicator(_failIndicator);
    }

    private void AnimateIndicator(GameObject indicatorObj)
    {
        indicatorObj.SetActive(true);

        Vector3 oriSize = indicatorObj.transform.localScale;
        indicatorObj.transform.localScale = Vector3.zero;
        indicatorObj.transform.DOScale(oriSize, _indicatorPopinTime);

        Util.WaitForSeconds(this, () =>
        {
            indicatorObj.transform.DOScale(Vector3.zero, _indicatorPopoutTime)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                indicatorObj.transform.localScale = oriSize;
                indicatorObj.gameObject.SetActive(false);
            })
            .SetEase(Ease.InBack);

        }, _indicatorPauseAppearTime);
    }
}