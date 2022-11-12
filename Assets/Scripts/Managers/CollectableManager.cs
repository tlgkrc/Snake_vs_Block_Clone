using Controllers;
using Controllers.Collectable;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables
        #region Public Variables

        #endregion
        #region SerializeField Variables
        [SerializeField] private CollectablePhysicController physicController;
        [SerializeField] private CollectableMeshController collectableMeshController;

        #endregion
        #region Private Variables
        
        
        #endregion
        #endregion


        #region Event Subscription

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

        public int GetIncreaseCount()
        {
            return 1;
        }
    }
}