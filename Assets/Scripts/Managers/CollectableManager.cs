using System;
using Controllers;
using Controllers.Collectable;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using TMPro;
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
        [SerializeField] private TextMeshPro scoreText;
        [SerializeField] private int score;

        #endregion
        #region Private Variables
        
        
        #endregion
        #endregion

        private void Awake()
        {
            scoreText.text = score.ToString();
        }
        
        public void InteractionWithPlayer()
        {
            StackSignals.Instance.onInteractionWithPlayer.Invoke(score);
            gameObject.SetActive(false);
        }
    }
}