using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mask : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _renderers;
    
    private MaskAddon _fullMaskAddon;

    private MaskAddon _currentMaskAddon;
    private int _addonsAdded;

    // Reference component
    private MaskApplication _maskApplication;
    public MaskAddon CurrentMaskAddon { get => _currentMaskAddon; }
    public MaskAddon FullMaskAddon { get => _fullMaskAddon; }

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
        _fullMaskAddon = maskAddon;
        _currentMaskAddon = new MaskAddon();

        DisplayBaseMask();
    }

    public bool CheckMaskCorrect()
    {
        return _fullMaskAddon.Equals(_currentMaskAddon);
    }

    private void DisplayBaseMask()
    {
        _currentMaskAddon.FaceBase = _fullMaskAddon.FaceBase;
        _renderers[0].sprite = _fullMaskAddon.FaceBase;
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