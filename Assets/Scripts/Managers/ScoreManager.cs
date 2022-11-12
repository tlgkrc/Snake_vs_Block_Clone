using Commands;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject stackGO;
        [SerializeField] private TextMeshPro scoreTMP, spriteTMP;
        [SerializeField] private GameObject textPlane;

        #endregion

        #region Private Variables

        private int _score, _idleScore,_idleOldScore;
        [ShowInInspector] private GameObject _playerGO;
        private bool _isActive = false;
        private int _savedScore;

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }
        
        private void Init()
        {
        }
        
        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
            ScoreSignals.Instance.onSetScore += OnUpdateScore;
            CoreGameSignals.Instance.onPlay += OnPlay;
            LevelSignals.Instance.onRestartLevel += OnReset;
            ScoreSignals.Instance.onGetIdleScore += OnGetCurrentScore;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
            ScoreSignals.Instance.onSetScore -= OnUpdateScore;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            LevelSignals.Instance.onRestartLevel -= OnReset;
            ScoreSignals.Instance.onGetIdleScore -= OnGetCurrentScore;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        

        private void Update()
        {
            SetScoreManagerRotation();
           
        }
        
        #region Event Methods

        private void OnPlay()
        {
            FindPlayerGameObject();
        }

        private void OnChangeGameState()
        {
            var transform1 = transform;
            transform1.parent = _playerGO.transform;
            transform1.localPosition = new Vector3(0, 2f, 0);
            ScoreSignals.Instance.onSetScore?.Invoke(_savedScore);
        }

        private void OnReset()
        {
            _isActive = false;
        }

        private void OnUpdateScore(int score)
        {
            
        }

        private int OnGetCurrentScore()
        {
            return _idleScore;
        }

        #endregion
        
        #region Methods

        private void SetScoreManagerRotation()
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z * -1f);
        }

        private void FindPlayerGameObject()
        {
            _playerGO = GameObject.FindGameObjectWithTag("Player");
            _isActive = true;
        }
        
        #endregion
    }
}