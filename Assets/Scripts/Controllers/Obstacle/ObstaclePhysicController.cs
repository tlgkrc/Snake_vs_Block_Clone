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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                manager.DecreaseHealth();
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.collider.CompareTag("Player"))
            {
                manager.StopDecreasingHealth();
            }
        }
    }
}