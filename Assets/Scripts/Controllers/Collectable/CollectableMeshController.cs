using System;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;
using DG.Tweening;
using Managers;

namespace Controllers

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
            StackSignals.Instance.onInteractionObstacle?.Invoke(manager.gameObject);
        }
    }
}