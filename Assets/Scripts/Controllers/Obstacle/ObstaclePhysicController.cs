using System;
using Managers;
using UnityEngine;

namespace Controllers.Obstacle
{
    public class ObstaclePhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private ObstacleManager manager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                manager.DecreaseHealth();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                manager.StopDecreasingHealth();
            }
        }
    }
}