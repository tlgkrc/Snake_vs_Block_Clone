using System;
using Commands;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public InputData Data;

        #endregion

        #region Serialized Variables

        [SerializeField] private bool isReadyForTouch, isFirstTimeTouchTaken;

        #endregion

        #region Private Variables

        private bool _isTouching;
        private float _currentVelocity; //ref type
        private Vector2? _mousePosition; //ref type
        private Vector3 _moveVector; //ref type
        private QueryPointerOverUIElementCommand _queryPointerOverUIElementCommand;

        #endregion

        #endregion
        
        private void Awake()
        {
            Data = GetInputData();
            Init();
        }

        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").InputData;

        private void Init()
        {
            _queryPointerOverUIElementCommand = new QueryPointerOverUIElementCommand();
        }

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            LevelSignals.Instance.onNextLevel += OnNextLevel;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            LevelSignals.Instance.onNextLevel -= OnNextLevel;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Update()
        {
            if (!isReadyForTouch) return;
            
            if (Input.GetMouseButtonUp(0) && _queryPointerOverUIElementCommand.Execute())
            {
                MouseButtonUp();
            }
            
            if (Input.GetMouseButtonDown(0) && !_queryPointerOverUIElementCommand.Execute())
            {
                MouseButtonDown();
            }
            
            if (Input.GetMouseButton(0) && !_queryPointerOverUIElementCommand.Execute())
            {
                HoldingMouseButton();
            }
        }

        #region Event Methods
        
        private void OnEnableInput()
        {
            isReadyForTouch = true;
        }
        
        private void OnDisableInput()
        {
            isReadyForTouch = false;
        }
        
        private void OnPlay()
        {
            isReadyForTouch = true;
        }

        private void OnReset()
        {
            _isTouching = false;
            isReadyForTouch = false;
            isFirstTimeTouchTaken = false;
        }

        private void OnNextLevel() 
        {
            _isTouching = false;
            isReadyForTouch = false;
            isFirstTimeTouchTaken = false;
        }

        #endregion
        
        #region InputUpdateMethods

        private void MouseButtonUp()
        {
            _isTouching = false;
            InputSignals.Instance.onInputReleased?.Invoke();
        }

        private void MouseButtonDown()
        {
            _isTouching = true;
            InputSignals.Instance.onInputTaken?.Invoke();
            if (!isFirstTimeTouchTaken)
            {
                isFirstTimeTouchTaken = true;
                InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
            }
            _mousePosition = Input.mousePosition;
        }

        private void HoldingMouseButton()
        {
            if (!_isTouching) return;
            if (_mousePosition == null) return;
            Vector2 mouseDeltaPos = (Vector2) Input.mousePosition - _mousePosition.Value;
                    
            if (mouseDeltaPos.x > Data.HorizontalInputSpeed)
                _moveVector.x = Data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
            else if (mouseDeltaPos.x < -Data.HorizontalInputSpeed)
                _moveVector.x = -Data.HorizontalInputSpeed / 10f * -mouseDeltaPos.x;
            else
                _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity,
                    Data.ClampSpeed);
                         
            _mousePosition = Input.mousePosition;
                         
            InputSignals.Instance.onRunnerInputDragged?.Invoke(new RunnerInputParams()
            {
                XValue = _moveVector.x,
                ClampValues = new Vector2(Data.ClampSides.x, Data.ClampSides.y)
            });
        }

        #endregion
    }
}