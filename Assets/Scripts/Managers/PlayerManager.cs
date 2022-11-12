using Commands;
using UnityEngine;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using Signals;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public PlayerData Data;

        #endregion

        #region Serialized Variables

        [Space] [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private PlayerMeshController meshController;

        #endregion

        #region Private Variables
        private Rigidbody _rb;
        private PlayerParticuleController _particuleController;


        #endregion
        #endregion

        private void Awake()
        {
            GetReferences();
            Init();
            SetStackPosition();
            SendPlayerDataToControllers();
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").Data;
        
        private void Init()
        {
            var transform1 = transform;
            _rb = GetComponent<Rigidbody>();
            _particuleController = GetComponent<PlayerParticuleController>();
        }

        private void GetReferences()
        {
            Data = GetPlayerData();
        }

        private void SendPlayerDataToControllers()
        {
            movementController.SetMovementData(Data.MovementData);
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            InputSignals.Instance.onInputReleased += OnDeactiveMovement;
            InputSignals.Instance.onRunnerInputDragged += OnSetRunnerInputValues;
            InputSignals.Instance.onJoystickDragged += OnSetIdleInputValues;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onChangeGameState += OnChangeMovementState;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            StackSignals.Instance.onSetPlayerScale += OnSetPlayerScale;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactiveMovement;
            InputSignals.Instance.onRunnerInputDragged -= OnSetRunnerInputValues;
            InputSignals.Instance.onJoystickDragged -= OnSetIdleInputValues;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onChangeGameState -= OnChangeMovementState;
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
            StackSignals.Instance.onSetPlayerScale -= OnSetPlayerScale;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        #region Event Methods

        #region Movement Controller

        private void OnActivateMovement()
        {
            movementController.EnableMovement();
        }

        private void OnDeactiveMovement()
        {
            movementController.DeactiveMovement();
        }

        private void OnSetRunnerInputValues(RunnerInputParams inputParams)
        {
            movementController.UpdateRunnerInputValue(inputParams);
        }
        
        private void OnSetIdleInputValues(IdleInputParams inputParams)
        {
            movementController.UpdateIdleInputValue(inputParams);
            animationController.SetSpeedVariable(inputParams);
        }

        private void OnChangeMovementState()
        {
            movementController.IsReadyToPlay(true);
            movementController.ChangeMovementState();
            movementController.EnableMovement();
            _rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        
        #endregion

        #region Others

        private void OnPlay()
        {
            SetStackPosition();
            movementController.IsReadyToPlay(true);
        }

        private void OnLevelSuccessful()
        {
            movementController.IsReadyToPlay(false);
            meshController.ShowSkinnedMesh();
        }
        
        private void OnLevelFailed()
        {
            movementController.IsReadyToPlay(false);
        }

        private void OnSetPlayerScale(float value)
        {
            animationController.SetPlayerScale(value);
        }
        private void OnReset()
        {
            gameObject.SetActive(true);
            movementController.OnReset();
            SetStackPosition();
        }

        private void OnInteractionBuyPoint(bool isInteractionBuyPoint, Transform targetTransform)
        {
            if (isInteractionBuyPoint)
            {
                ParticuleState(true, targetTransform);
            }
            else
            {
                ParticuleState(false);
            }
        }

        #endregion

        #endregion

        #region Methods

        private void SetStackPosition()
        {
            StackSignals.Instance.onPlayerGameObject?.Invoke(gameObject);
        }
        
        private void ParticuleState(bool active, Transform instantiateTransform = null)
        {
            if (active)
            {
                _particuleController.StartParticule(instantiateTransform);
            }
            else
            {
                _particuleController.StopParticule();
            }
        }

        #endregion
    }
}
