using Controllers.Obstacle;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
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
        
        #endregion

        #region Private Variables

        private ColorData _colorData;
        private ushort _health;

        #endregion

        #endregion


        private void Awake()
        {
            _colorData = GetColorData();
        }

        #region Event Supscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            LevelSignals.Instance.onSetObstacleScore += OnSetObstacleScore;
        }

        private void UnsubscribeEvents()
        {
            LevelSignals.Instance.onSetObstacleScore -= OnSetObstacleScore;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            SendDataToControllers();
        }

        private ColorData GetColorData()
        {
            return Resources.Load<CD_Color>("Data/CD_Color").colorData;
        }

        private void SendDataToControllers()
        {
            healthController.SetHealthData(_health);
            meshController.SetHealthData(_colorData,_health);
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

        private void OnSetObstacleScore(int health,GameObject newGO)
        {
            if (gameObject == newGO)
            {
                _health = (ushort)health;
            }
        }
    }
}