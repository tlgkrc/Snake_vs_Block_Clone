using System;
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

        [SerializeField] private TextMeshPro stackScoreTMP;

        #endregion

        #region Private Variables

        private int _score;
        [ShowInInspector] private GameObject _playerGO;
        private bool _isActive;
        private int _savedScore;

        #endregion

        #endregion

        private void Awake()
        {
            
        }
        
        
        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onSetScore += OnUpdateScore;
            CoreGameSignals.Instance.onPlay += OnPlay;
            LevelSignals.Instance.onRestartLevel += OnReset;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onSetScore -= OnUpdateScore;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            LevelSignals.Instance.onRestartLevel -= OnReset;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            var newScore = StackSignals.Instance.onGetCurrentScore?.Invoke();
            OnUpdateScore((int)newScore);
        }

        private void Update()
        {
            if (_playerGO!=null)
            {
                SetScoreManagerPosition();
            }
        }
        
        #region Event Methods

        private void OnPlay()
        {
            FindPlayerGameObject();
        }
        
        private void OnReset()
        {
            _isActive = false;
        }

        private void OnUpdateScore(int score)
        {
            stackScoreTMP.text = _score.ToString();
        }

        #endregion
        
        #region Methods

        private void FindPlayerGameObject()
        {
            _playerGO = GameObject.FindGameObjectWithTag("Player");
            _isActive = true;
        }
        
        private void SetScoreManagerPosition()
        {
            transform.position = _playerGO.transform.position + new Vector3(0, 0, 1.2f);
        }
        
        #endregion
    }
}