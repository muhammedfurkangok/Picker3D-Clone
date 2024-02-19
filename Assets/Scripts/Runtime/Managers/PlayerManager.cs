using Runtime.Commands.Player;
using Runtime.Controllers.Player;
using Runtime.Controllers.UI;
using Runtime.Data.UnityObjects;
using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    
public class PlayerManager: MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    public byte StageValue;
    
    internal ForceBallsToPoolCommand ForceCommand;

    #endregion

    #region Serialized Veriables

    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private PlayerMeshController meshController;
    [SerializeField] private PlayerPhysicController physicController;

    #endregion

    #region Private Variables

    private PlayerData _data;

    #endregion

    #endregion

    private void Awake()
    {
        _data = GetPlayerData();
        SendDataToController();
        Init();

    }

    private void Init()
    {
        ForceCommand = new ForceBallsToPoolCommand(this, _data.ForceData);
    }

    private void SendDataToController()
    {
        movementController.SetData(_data.MovementData);
        meshController.SetData(_data.MeshData);
      
    }

    private PlayerData GetPlayerData()
    {
        return Resources.Load<CD_Player>("Data/CD_Player").Data;
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        InputSignals.Instance.onInputTaken += OnInputTaken;
        InputSignals.Instance.onInputReleased += OnInputReleased;
        InputSignals.Instance.onInputDragged += OnInputDragged;
        UISignals.Instance.onPlay += OnPlay;
        CoreGameSignals.Instance.onLevelSuccesful += OnLevelSuccesful;
        CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
        CoreGameSignals.Instance.onStageAreaEntered += OnStageAreaEntered;
        CoreGameSignals.Instance.onStageAreaSuccessful += OnStageAreaSuccessful;
        CoreGameSignals.Instance.onFinishAreaEntered += OnFinishedAreaEntered;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void OnPlay()
    {
        movementController.IsReadyToPlay(true);
    }
    private void OnInputTaken()
    {
        movementController.IsReadyToMove(true);
    }
    private void OnInputDragged(HorizontalInputParams inputParams)
    {
        movementController.UpdateInputParams(inputParams);
    }
    private void OnInputReleased()
    {
        movementController.IsReadyToMove(false);
    }

    private void OnFinishedAreaEntered()
    {
            CoreGameSignals.Instance.onLevelSuccesful?.Invoke();
            //minigame yazicam
    }
    private void OnStageAreaEntered()
    {
        movementController.IsReadyToPlay(false);
        
    }
    private void OnStageAreaSuccessful(byte value)
    {
        StageValue = (byte)++value;
    }

    private void OnLevelSuccesful()
    {
        
        movementController.IsReadyToPlay(false);  
    }

    private void OnLevelFailed()
    {
        movementController.IsReadyToPlay(false);
    }
    
    private void OnReset()
    {
        StageValue = 0;
        movementController.OnReset();
        physicController.OnReset();
        meshController.OnReset();
    }

    private void UnSubscribeEvents()
    {
        InputSignals.Instance.onInputTaken -= OnInputTaken;
        InputSignals.Instance.onInputReleased -= OnInputReleased;
        InputSignals.Instance.onInputDragged -= OnInputDragged;
        UISignals.Instance.onPlay -= OnPlay;
        CoreGameSignals.Instance.onLevelSuccesful -= OnLevelSuccesful;
        CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
        CoreGameSignals.Instance.onStageAreaEntered -= OnStageAreaEntered;
        CoreGameSignals.Instance.onStageAreaSuccessful -= OnStageAreaSuccessful;
        CoreGameSignals.Instance.onFinishAreaEntered -= OnFinishedAreaEntered;
        CoreGameSignals.Instance.onReset -= OnReset;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
}
