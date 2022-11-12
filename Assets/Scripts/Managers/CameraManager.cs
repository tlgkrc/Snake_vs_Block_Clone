using Cinemachine;
using Enums;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CinemachineVirtualCamera levelCamera;

        #endregion
        
        #region Private Variables

        private Animator _camAnimator;
        private CameraStates _cameraState;

        #endregion

        #endregion
        
        private void Awake()
        {
            GetReferences();
        }

        private void Start()
        {
            OnSetCameraTarget();
        }

        private void GetReferences()
        {
            _camAnimator = GetComponent<Animator>();
        }

        #region Event Subscriptions
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            
        }

        private void UnsubscribeEvents()
        {
            
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void SetCameraStates()
        {
            if (_cameraState == CameraStates.LevelCam)
            {
                _camAnimator.Play(CameraStates.LevelCam.ToString());
            }
        }

        private void OnSetCameraState(CameraStates cameraState)
        {
            _cameraState = cameraState;
            SetCameraStates();
        }
        
        private void OnSetCameraTarget()
        {
            var playerManager = FindObjectOfType<PlayerManager>().transform;
            levelCamera.Follow = playerManager;
        }
     
        private void OnReset()
        {
            _cameraState = CameraStates.LevelCam;
            levelCamera.Follow = null; 
            levelCamera.LookAt = null;
        }
    }
}