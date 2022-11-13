using System;
using UnityEngine;
using Controllers;
using Controllers.Player;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
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
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            StackSignals.Instance.onInteractionWithObstacle += OnInteractionWithObstacle;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactiveMovement;
            InputSignals.Instance.onRunnerInputDragged -= OnSetRunnerInputValues;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
            StackSignals.Instance.onInteractionWithObstacle -= OnInteractionWithObstacle;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            SetStackPosition();
        }

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
        
        
        #endregion

        #region Others

        private void OnPlay()
        {
            movementController.IsReadyToPlay(true);
        }

        private void OnLevelSuccessful()
        {
            movementController.IsReadyToPlay(false);
        }
        
        private void OnLevelFailed()
        {
            movementController.IsReadyToPlay(false);
            gameObject.SetActive(false);
        }

        private void OnSetPlayerScale(float value)
        {
            //animationController.SetPlayerScale(value);
        }
        private void OnReset()
        {
            gameObject.SetActive(true);
            movementController.OnReset();
            SetStackPosition();
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

        private void OnInteractionWithObstacle()
        {
            transform.DOMoveZ(transform.position.z - .5f, 0.2f);
        }

        #endregion
    }
}
