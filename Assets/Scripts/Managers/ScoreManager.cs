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

        private int _totalScore;
        private int _score;
        private GameObject _playerGameObject;
        
        #endregion

        #endregion
        
        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onUpdateTotalScore += OnUpdateTotalScore;
            ScoreSignals.Instance.onUpdateStackScore += OnUpdateStackScore;
            CoreGameSignals.Instance.onPlay += OnPlay;
            LevelSignals.Instance.onRestartLevel += OnReset;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onUpdateTotalScore -= OnUpdateTotalScore;
            ScoreSignals.Instance.onUpdateStackScore -= OnUpdateStackScore;
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
            int newScore = (int)StackSignals.Instance.onGetCurrentScore?.Invoke();
            OnUpdateStackScore(newScore);
        }

        private void Update()
        {
            if (_playerGameObject!=null)
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
        }

        private void OnUpdateStackScore(int score)
        {
            stackScoreTMP.text = score.ToString();
        }
        
        private void OnUpdateTotalScore(int value)
        {
            _totalScore += value;
            UISignals.Instance.onUpdateTotalScoreText?.Invoke(_totalScore);
        }

        #endregion
        
        #region Methods

        private void FindPlayerGameObject()
        {
            _playerGameObject = GameObject.FindGameObjectWithTag("Player");
        }
        
        private void SetScoreManagerPosition()
        {
            transform.position = _playerGameObject.transform.position + new Vector3(0, 0, .8f);
        }
        
        #endregion
    }
}