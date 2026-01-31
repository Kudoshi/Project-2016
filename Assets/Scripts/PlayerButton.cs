
using TMPro;
using UnityEngine;

public class PlayerButton : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _colorBtnText;
    [SerializeField] private SO_MaskData _maskDataSO;

    public void UpdateButtonColor(MaskAddonType type, Sprite sprite)
    {
        ColorType colorType = _maskDataSO.GetColorTypeBySprite(type, sprite);
        Sprite buttonSprite = _maskDataSO.GetButtonSprite(colorType);

        _colorBtnText.sprite = buttonSprite;
    }
}