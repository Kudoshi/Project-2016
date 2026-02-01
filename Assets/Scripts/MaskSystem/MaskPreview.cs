using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MaskPreview : MonoBehaviour
{
    [SerializeField] private Image faceBaseImage;
    [SerializeField] private Image faceAddonImage;
    [SerializeField] private Image eyesAddonImage;
    [SerializeField] private Image mouthAddonImage;
    [SerializeField] private Image accessoryAddonImage;
    [SerializeField] private float popinTime;

    public void Show(MaskAddon maskAddon)
    {
        Vector3 originalSize = transform.localScale;

        transform.localScale = Vector3.zero;
        transform.DOScale(originalSize, popinTime).SetEase(Ease.OutBack);

        faceBaseImage.sprite = maskAddon.FaceBase;
        faceAddonImage.sprite = maskAddon.FaceAddon;
        eyesAddonImage.sprite = maskAddon.EyesAddon;
        mouthAddonImage.sprite = maskAddon.MouthAddon;
        accessoryAddonImage.sprite = maskAddon.AccessoryAddon;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}