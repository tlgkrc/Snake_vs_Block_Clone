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
    
        private CollectableData _data;
        private ColorData _colorData;
        #endregion
        #endregion

        private void Awake()
        {
            _data = GetCollectableData();
        }

        private CollectableData GetCollectableData() => Resources.Load<CD_Collectable>("Data/CD_Collectable").Data;

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
        
        public void InteractionWithCollectable(GameObject collectableGameObject)
        {
            collectableMeshController.ColorControl(collectableGameObject);
        }

        public void InteractionWithObstacle(GameObject collectableGameObject)
        {
            StackSignals.Instance.onInteractionObstacle?.Invoke(collectableGameObject);
        }
    }
}