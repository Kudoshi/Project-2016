using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mask : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _renderers;
    
    private MaskAddon _maskAddon;

    private MaskAddon _currentMaskAddon;
    private int _addonsAdded;

    // Reference component
    private MaskApplication _maskApplication;

    public MaskAddon FullMaskAddon { get => _maskAddon; }
    public MaskAddon CurrentMaskAddon { get => _currentMaskAddon; }

    private void Awake()
    {
        _maskApplication = GetComponent<MaskApplication>();
    }

    public void ApplyMaskAddon(Sprite addonSprite)
    {
        _renderers[_addonsAdded].sprite = addonSprite;

        UpdateMaskData(addonSprite);
        _addonsAdded++;

    }

    public void InitializeMaskAddon(MaskAddon maskAddon)
    {
        _currentMaskAddon = maskAddon;

        DisplayBaseMask();
    }

    public bool CheckMaskCorrect()
    {
        return _maskAddon == _currentMaskAddon;
    }

    private void DisplayBaseMask()
    {
        _renderers[0].sprite = _currentMaskAddon.FaceBase;
        _addonsAdded++;
    }

    private void UpdateMaskData(Sprite addonSprite)
    {
        if (_currentMaskAddon.FaceAddon == null)
        {
            _currentMaskAddon.FaceAddon = addonSprite;
        }
        else if (_currentMaskAddon.EyesAddon == null)
        {
            _currentMaskAddon.EyesAddon = addonSprite;
        }
        else if (_currentMaskAddon.MouthAddon == null)
        {
            _currentMaskAddon.MouthAddon = addonSprite;
        }
        else if (_currentMaskAddon.AccessoryAddon == null)
        {
            _currentMaskAddon.AccessoryAddon = addonSprite;
        }
    }

}