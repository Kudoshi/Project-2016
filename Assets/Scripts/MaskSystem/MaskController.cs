

using Kudoshi.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaskController : Singleton<MaskController>
{
    [SerializeField] private Mask _maskPf;
    [SerializeField] private SO_MaskData _maskDataSO;
    [SerializeField] private PlayerInput[] _playerInputList;

    private FactoryState _factoryState = FactoryState.IDLE;

    //private 

    private int _maskCompleted = 0;
    private Mask _currentMask;

    private void Start()
    {
        SpawnMask();
    }

    public void SetFactoryState(FactoryState factoryState)
    {
        _factoryState = factoryState;
    }

    private void SpawnMask()
    {
        //SetFactoryState(FactoryState.MASK_PREPARING);

        _currentMask = Instantiate(_maskPf);
        MaskAddon maskAddon = _maskDataSO.GenerateMaskAddon();
        _currentMask.InitializeMaskAddon(maskAddon);

        // Currently just tp to middle
        _currentMask.transform.position = new Vector3(0, 0, 0);

        AssignMaskAddOnForPlayers();
        
    }

    private void AssignMaskAddOnForPlayers()
    {
        List<int> playerIndexList = new List<int>() { 0, 1, 2, 3};

        for (int i = playerIndexList.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (playerIndexList[i], playerIndexList[j]) = (playerIndexList[j], playerIndexList[i]);
        }

        int assigningIndex = 0;
        foreach (int playerIndex in playerIndexList)
        {
            MaskAddonType addonType;
            Sprite spriteToAssign;
            if (assigningIndex == 0)
            {
                spriteToAssign = _currentMask.FullMaskAddon.FaceAddon;
                addonType = MaskAddonType.FACE_ADDON;
            }
            else if (assigningIndex == 1)
            {
                spriteToAssign = _currentMask.FullMaskAddon.EyesAddon;
                addonType = MaskAddonType.EYES_ADDON;
            }
            else if (assigningIndex == 2)
            {
                spriteToAssign = _currentMask.FullMaskAddon.MouthAddon;
                addonType = MaskAddonType.MOUTH_ADDON;
            }
            else
            {
                spriteToAssign = _currentMask.FullMaskAddon.AccessoryAddon;
                addonType = MaskAddonType.ACCESSORY_ADDON;
            }

            _playerInputList[playerIndex].AssignAddon(addonType, spriteToAssign);

            assigningIndex++;
        }
    }

    public void InputApplyAddon(Sprite maskAddon)
    {
        _currentMask.ApplyMaskAddon(maskAddon);
    }
}

public enum FactoryState
{
    IDLE, MASK_PREPARING, MASK_READY, MASK_END, END_GAME
}