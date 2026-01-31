
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode _keyCode;
    [SerializeField] private int _playerIndex;
    [SerializeField] private PlayerButton _playerButton;

    private Sprite _maskAddon;
    private MaskAddonType _maskAddonType;
    private bool _canInput;

    private void Update()
    {
        if (Input.GetKeyDown(_keyCode) && _canInput)
        {
            MaskController.Instance.InputApplyAddon(_maskAddon);
            _canInput = false;
        }
    }

    public void AssignAddon(MaskAddonType addonType, Sprite addon)
    {
        _maskAddon = addon;
        _maskAddonType = addonType;
        _canInput = true;
        _playerButton.UpdateButtonColor(addonType, addon);
    }
}
