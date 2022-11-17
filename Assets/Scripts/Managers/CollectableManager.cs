using System;
using Controllers.Collectable;
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

        #endregion
        #region Private Variables

        private ushort _score;
        
        #endregion
        #endregion

        #region Self Variables

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            LevelSignals.Instance.onSetCollectableScore += OnSetCollectableScore;
        }

        private void UnsubscribeEvents()
        {
            LevelSignals.Instance.onSetCollectableScore -= OnSetCollectableScore;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        public void InteractionWithPlayer()
        {
            StackSignals.Instance.onInteractionWithPlayer.Invoke(_score);
            gameObject.SetActive(false);
        }

        private void OnSetCollectableScore(int value,GameObject newGO)
        {
            if (gameObject != newGO) return;
            _score = (ushort)value;
            scoreText.text = value.ToString();
        }
    }
}