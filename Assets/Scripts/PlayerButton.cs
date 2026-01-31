
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerButton : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _colorBtnText;
    [SerializeField] private SO_MaskData _maskDataSO;
    [SerializeField] private float _btnPressScale;
    [SerializeField] private float _btnPressDuration;
    [SerializeField] private float _btnUnpressDuration;

    private float _btnOriginalPressScale;

    private void Awake()
    {
        _btnOriginalPressScale = _colorBtnText.transform.localScale.y;
    }

    public void UpdateButtonColor(MaskAddonType type, Sprite sprite)
    {
        ColorType colorType = _maskDataSO.GetColorTypeBySprite(type, sprite);
        Sprite buttonSprite = _maskDataSO.GetButtonSprite(colorType);

        _colorBtnText.sprite = buttonSprite;
    }

    public void ButtonPress()
    {
        _colorBtnText.DOKill();
        _colorBtnText.transform.DOScaleY(_colorBtnText.transform.localScale.y * _btnPressScale, _btnPressDuration)
        .OnComplete(() =>
        {
            _colorBtnText.transform.DOScaleY(_btnOriginalPressScale, _btnUnpressDuration);
        });
    }
}