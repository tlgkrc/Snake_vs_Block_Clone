using System;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public int Data;

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject levelHolder;
        [SerializeField] private LevelGenerateController generateController;

        #endregion

        #region Private Variables

        private LevelData _levelData;
        private float _playerPosZ;
        
        #endregion

        #endregion

        private void Awake()
        {
            _levelData = GetLevelData();
            SetDataToControllers();
        }
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            ScoreSignals.Instance.onSetLeadPosition += OnSetPlayerPos;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            ScoreSignals.Instance.onSetLeadPosition -= OnSetPlayerPos;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Update()
        {
            generateController.UpdateLevel((float)LevelSignals.Instance.onGetPlayerPos?.Invoke().z);
        }

        private LevelData GetLevelData()
        {
            return Resources.Load<CD_Level>("Data/CD_Level").LevelData;
        }

        private void OnSetPlayerPos(GameObject player)
        {
            _playerPosZ = player.transform.position.z;
            generateController.UpdateLevel(_playerPosZ);
        }

        private void SetDataToControllers()
        {
            generateController.SetData(_levelData);
        }

        private void OnPlay()
        {
            generateController.InitializeLevel();
        }
    }
}