using UnityEngine;
using UnityEngine.UI;

public class MaskPreview : MonoBehaviour
{
    [SerializeField] private Image faceBaseImage;
    [SerializeField] private Image faceAddonImage;
    [SerializeField] private Image eyesAddonImage;
    [SerializeField] private Image mouthAddonImage;
    [SerializeField] private Image accessoryAddonImage;

    public void Show(MaskAddon maskAddon)
    {
        faceBaseImage.sprite = maskAddon.FaceBase;
        faceAddonImage.sprite = maskAddon.FaceAddon;
        eyesAddonImage.sprite = maskAddon.FaceAddon;
        mouthAddonImage.sprite = maskAddon.MouthAddon;
        accessoryAddonImage.sprite = maskAddon.AccessoryAddon;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}