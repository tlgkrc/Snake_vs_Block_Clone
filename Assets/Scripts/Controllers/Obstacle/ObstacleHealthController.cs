using System;
using System.Collections;
using Managers;
using TMPro;
using UnityEngine;

namespace Controllers.Obstacle
{
    public class ObstacleHealthController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private ObstacleManager manager;
        [SerializeField] private TextMeshPro healthText;

        #endregion

        #region Private Variables

        private bool _isTriggered;
        private ushort _health;

        #endregion

        #endregion

        private void Awake()
        {
            _isTriggered = false;
        }

        private void SetHealthText()
        {
            healthText.text = _health.ToString();
        }

        public IEnumerator InteractionWithPlayer()
        {
            while (_isTriggered)
            {
                if (_health>0)
                {
                    _health--;
                    SetHealthText();
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    manager.DestroyedState();
                    yield break;
                }
            }
        }

        public void SetTriggerState(bool isTriggered)
        {
            _isTriggered = isTriggered;
        }

        public void SetHealthData(ushort health)
        {
            _health = health;
            SetHealthText();
        }
    }
}