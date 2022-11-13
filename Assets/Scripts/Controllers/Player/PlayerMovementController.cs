using Data.ValueObject;
using Keys;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables
        
        #region Serialized Variables
        
        [SerializeField] private new Rigidbody rigidbody;
        
        #endregion
        
        #region Private Variables
        
        private PlayerMovementData _movementData;
        private bool _isReadyToMove, _isReadyToPlay;
        private float _inputValue;
        private Vector2 _clampValues;
        
        #endregion
        
        #endregion

        public void SetMovementData(PlayerMovementData dataMovementData)
        {
            dataMovementData.ForwardSpeed = dataMovementData.RunSpeed;
            _movementData = dataMovementData;
        }

        public void EnableMovement()
        {
            _isReadyToMove = true;
        }

        public void DeactiveMovement()
        {
            _isReadyToMove = false;
        }

        public void UpdateRunnerInputValue(RunnerInputParams inputParam)
        {
            _inputValue = inputParam.XValue;
            _clampValues = inputParam.ClampValues;
        }

        public void IsReadyToPlay(bool state)
        {
            _isReadyToPlay = state;
        }
        
        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            { 
                if (_isReadyToMove)
                {
                    Move();
                }
                else
                {
                    StopSideways();
                }
            }
            else
            { 
                Stop();
            }
        }

        private void Move()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_inputValue * _movementData.SidewaysSpeed, velocity.y,
                _movementData.ForwardSpeed);
            rigidbody.velocity = velocity;
            
            Vector3 position;
            position = new Vector3(Mathf.Clamp(rigidbody.position.x, _clampValues.x,
                _clampValues.y), (position = rigidbody.position).y, position.z);
            rigidbody.position = position;

            Transform transform1;
            (transform1 = transform).Rotate(0,_inputValue/4,0,Space.Self);
            var transformEulerAngles = transform1.eulerAngles;
            transformEulerAngles.y = Mathf.Clamp(transformEulerAngles.y, 
                _movementData.ClampRotation.x,_movementData.ClampRotation.y);
        }

        private void StopSideways()
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _movementData.ForwardSpeed);
            var to = Quaternion.Euler(Vector3.zero);
            transform.rotation = Quaternion.Lerp(transform.rotation, to,  _movementData.Damping);
            rigidbody.angularVelocity = Vector3.zero;
        }

        private void Stop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        public void OnReset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }
    }
}