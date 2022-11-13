using Data.UnityObject;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Commands.Level;

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

        #endregion

        #region Private Variables

        private LevelLoaderCommand _levelLoader;
        private ClearActiveLevelCommand _levelClearer;
        [ShowInInspector] private int _levelID;

        #endregion

        #endregion

        private void Awake()
        {
            _levelID = GetActiveLevel();
            _levelClearer = new ClearActiveLevelCommand();
            _levelLoader = new LevelLoaderCommand();
        }

        private int GetActiveLevel()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("Level") ? ES3.Load<ushort>("Level") : 0;
        }

        private int GetLevelCount()
        {
            return (_levelID % Resources.Load<CD_Level>("Data/CD_Level").Levels.Count);
           
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            LevelSignals.Instance.onLevelInitialize += OnInitializeLevel;
            LevelSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            LevelSignals.Instance.onNextLevel += OnNextLevel;
            LevelSignals.Instance.onRestartLevel += OnRestartLevel;
            LevelSignals.Instance.onNextLevelInitialize += OnNextLevelInitialize;
        }

        private void UnsubscribeEvents()
        {
            LevelSignals.Instance.onLevelInitialize -= OnInitializeLevel;
            LevelSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            LevelSignals.Instance.onNextLevel -= OnNextLevel;
            LevelSignals.Instance.onRestartLevel -= OnRestartLevel;
            LevelSignals.Instance.onNextLevelInitialize -= OnNextLevelInitialize;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            OnInitializeLevel();
            SetLevelText();
        }

        private void OnNextLevel()
        {
            _levelID++;
            LevelSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            LevelSignals.Instance.onLevelInitialize?.Invoke();
            SetLevelText();
        }

        private void OnRestartLevel()
        {
            LevelSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            LevelSignals.Instance.onLevelInitialize?.Invoke();
        }
        private int OnGetLevelID()
        {
            return _levelID;
        }

        private void SetLevelText()
        {
            UISignals.Instance.onSetLevelText?.Invoke(_levelID);
        }
        private void OnInitializeLevel()
        {
            int newLevelData = GetLevelCount();
            _levelLoader.InitializeLevel(newLevelData, levelHolder.transform);
        }
        private void OnClearActiveLevel()
        {
            _levelClearer.ClearActiveLevel(levelHolder.transform);
        }

        private void OnNextLevelInitialize(Vector3 pos)
        {
            _levelID++;
            _levelLoader.InitializeLevel(_levelID, levelHolder.transform);
            levelHolder.transform.GetChild(1).transform.position = pos;
            if (levelHolder.transform.childCount >= 2)
            {
                _levelClearer.ClearActiveLevel(levelHolder.transform.GetChild(0));
            }
        }
    }
}