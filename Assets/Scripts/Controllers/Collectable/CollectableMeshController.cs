using Data.ValueObject;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Collectable

{
    public class CollectableMeshController: MonoBehaviour
    {
        #region Self Variables
        #region Serializefield Variables
        [SerializeField] private CollectableManager manager;
        //[SerializeField] private SkinnedMeshRenderer mesh;
        #endregion
        #region Private Variables
        private ColorData _colorData;
        #endregion
        #endregion

        public void ColorControl(GameObject otherGameObject)
        {
            CollectableManager cm = otherGameObject.transform.parent.gameObject.GetComponent<CollectableManager>();
            otherGameObject.transform.parent.gameObject.SetActive(false);
            //StackSignals.Instance.onInteractionObstacle?.Invoke(manager.gameObject);
        }
    }
}