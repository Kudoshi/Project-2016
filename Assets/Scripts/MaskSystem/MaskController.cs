

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
    [SerializeField] private MaskPreview _maskPreview;
    [SerializeField] private PlayerInput[] _playerInputList;

    [Header("Points")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private Transform exitPoint;
    [SerializeField] private float moveDuration = 1f;

    private FactoryState _factoryState = FactoryState.IDLE;

    //private 

    private int _addonAdded = 0;
    private Mask _currentMask;

    public FactoryState FactoryState { get => _factoryState; }
    public Mask CurrentMask { get => _currentMask; }

    private void OnEnable()
    {
        GameManager.OnChangeGameState += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnChangeGameState -= OnGameStateChanged;

    }
    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.GAME)
        {
            SpawnMask();
            
        }
           
        
    }

    public void SetFactoryState(FactoryState factoryState)
    {
        _factoryState = factoryState;
    }

    public void MaskDiscard()
    {
        Destroy(_currentMask.gameObject);
        _currentMask = null;
    }

    public void SpawnMask()
    {
        if (GameManager.Instance.GameState != GameState.GAME) return;

        _addonAdded = 0;
        SetFactoryState(FactoryState.MASK_PREPARING);

        _currentMask = Instantiate(_maskPf);
        MaskAddon maskAddon = _maskDataSO.GenerateMaskAddon();
        _currentMask.InitializeMaskAddon(maskAddon);
        _maskPreview.Show(_currentMask.FullMaskAddon);

        // Currently just tp to middle
        _currentMask.transform.position = spawnPoint.position;
        var mover = _currentMask.GetComponent<MaskMover>();
        mover.MoveTo(interactionPoint.position, moveDuration, ()
            =>
        {
            MaskArrived();
        });

        AssignMaskAddOnForPlayers();
    }

   

   

    #region Mask Events
    private void MaskArrived()
    {
        SetFactoryState(FactoryState.MASK_READY);
        GameManager.Instance.OnMaskArrived();
    }

    // Do checking for mask here
    private void MaskSubmit()
    {
        if (_currentMask.CheckMaskCorrect())
        {
            GameManager.Instance.UpdateMaskSuccess();
            // We do things that are true here
        }
        else
        {
            GameManager.Instance.UpdateMaskFail();
        }

        SpawnMask();
    }

    private void MaskDoneDeliver()
    {
        SetFactoryState(FactoryState.MASK_END);

        var mover = _currentMask.GetComponent<MaskMover>();
        mover.MoveTo(exitPoint.position, moveDuration, () =>
        {
            Destroy(_currentMask.gameObject);
            MaskSubmit();
        });
    }


    #endregion
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

    public bool InputApplyAddon(Sprite maskAddon)
    {
        if (_factoryState != FactoryState.MASK_READY) return false;

        _currentMask.ApplyMaskAddon(maskAddon);
        _addonAdded++;

        if (_addonAdded >= 4)
        {
            MaskDoneDeliver();
        }

        return true;
    }
}

public enum FactoryState
{
    IDLE, MASK_PREPARING, MASK_READY, MASK_END
}