
using System;
using UnityEngine;

public class MaskApplication : MonoBehaviour
{
    [SerializeField] private Mask _mask;
    [SerializeField] private SO_MaskData _maskDataSO;

    public void ApplyMaskVisual(int maskLayerIndex, MaskAddon addon)
    {
        GetMaskApplicationType(addon);
    }

    private void GetMaskApplicationType(MaskAddon addon)
    {
        if (addon.ToString().Split("_")[0] == "TEXTURE")
        {
            ApplyTexture(addon);
        }
    }

    private void ApplyTexture(MaskAddon addon)
    {
        //Texture2D texture = _maskDataSO.GetMaskAddonTexture(addon);
        //string materialKey = "_MaskOverlay" + (_textureApplicationCount + 1);
        //_material.SetTexture(materialKey, texture);
        //_textureApplicationCount++;
    }
}
