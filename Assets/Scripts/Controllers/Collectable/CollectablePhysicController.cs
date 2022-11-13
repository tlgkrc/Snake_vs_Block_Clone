using DG.Tweening;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Collectable
{
    public class CollectablePhysicController : MonoBehaviour
    {
        #region Self Variables
        #region Serializefield Variables
        [SerializeField] private CollectableManager manager;
        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                manager.InteractionWithPlayer();
            }
        }
    }
}

