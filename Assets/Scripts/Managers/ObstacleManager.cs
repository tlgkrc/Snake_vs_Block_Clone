using System;
using Controllers.Obstacle;
using Data.UnityObject;
using Data.ValueObject;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ObstacleManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private ObstacleHealthController healthController;
        [SerializeField] private ObstaclePhysicController physicController;
        [SerializeField] private ObstacleMeshController meshController;
        [SerializeField] private ushort health;
        
        #endregion

        #region Private Variables

        private ColorData _colorData;

        #endregion

        #endregion


        private void Awake()
        {
            _colorData = GetColorData();
            SendDataToControllers();
        }

        private ColorData GetColorData()
        {
            return Resources.Load<CD_Color>("Data/CD_Color").colorData;
        }

        private void SendDataToControllers()
        {
            healthController.SetHealthData(health);
            meshController.SetHealthData(_colorData,health);
        }

        public void DecreaseHealth()
        {
            healthController.SetTriggerState(true);
            StartCoroutine(healthController.InteractionWithPlayer());
        }

        public void StopDecreasingHealth()
        {
            healthController.SetTriggerState(false);
            healthController.StopCoroutine(healthController.InteractionWithPlayer());
        }

        public void DestroyedState()
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
    }
}