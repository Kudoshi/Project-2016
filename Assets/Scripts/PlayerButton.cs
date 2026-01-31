
using TMPro;
using UnityEngine;

public class PlayerButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _colorBtnText;
    [SerializeField] private SO_MaskData _maskDataSO;

    public void UpdateButtonColor(MaskAddonType type, Sprite sprite)
    {
        ColorType colorType = _maskDataSO.GetColorTypeBySprite(type, sprite);

        _colorBtnText.text = colorType.ToString() + " | " + sprite.name.ToString();
    }
}