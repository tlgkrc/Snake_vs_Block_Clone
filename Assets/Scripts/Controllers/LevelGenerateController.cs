using System.Collections.Generic;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class LevelGenerateController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private Transform levelHolder;

        #endregion

        #region Private Variables

        private bool _preRowHasObstacle;
        private bool _smallNumberExistInBlock;
        private float _scaleOfObstacle;
        private ushort _blockCount;
        private ushort _posCounter;
        private List<GameObject> _obstacleList = new List<GameObject>();
        private List<GameObject> _collectableList = new List<GameObject>();
        private List<GameObject> _lineList = new List<GameObject>();
        private LevelData _levelData;

        #endregion

        #endregion

        public void SetData(LevelData levelData)
        {
            _levelData = levelData;
            _scaleOfObstacle = _levelData.Obstacle.transform.GetChild(0).transform.localScale.z;
        }

        public void InitializeLevel()
        {
            for (int i = 0; i < _levelData.MaxCubeOnALine; i++)
            {
                AddNewLine();
            }
        }

        private void CalculateObstacleText(bool isBlock,GameObject gO)
        {
            if (isBlock! || !_smallNumberExistInBlock)
            {
                var newScore = Random.Range(1, _levelData.maxObstacleScore);
                LevelSignals.Instance.onSetObstacleScore?.Invoke(newScore,gO);
                if (newScore<=10)
                {
                    _smallNumberExistInBlock = true;
                }
                return;
            }

            var refreshedScore = Random.Range(1, 10);
            LevelSignals.Instance.onSetObstacleScore?.Invoke(refreshedScore,gO);
            _smallNumberExistInBlock = true;

        }

        public void UpdateLevel(float playerPosZ)
        {
            UpdateObstacles(playerPosZ);
            UpdateCollectables(playerPosZ);
        }

        private void UpdateCollectables(float playerPosZ)
        {
            if (playerPosZ > _collectableList[0].transform.position.z + _scaleOfObstacle * _levelData.LineCount)
            {
                PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolTypes.Collectable.ToString(), _collectableList[0]);
                _collectableList.RemoveAt(0);
                _collectableList.TrimExcess();
            }
        }

        private void UpdateObstacles(float playerPosZ)
        {
            if (playerPosZ > _obstacleList[0].transform.position.z + _scaleOfObstacle*_levelData.LineCount)
            {
                PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolTypes.Obstacle.ToString(), _obstacleList[0]);
                _obstacleList.RemoveAt(0);
                _obstacleList.TrimExcess();
                AddNewLine();
            }
        }

        private void AddNewLine()
        {
            var random = new System.Random();
            var newProbForBlock = (float)random.NextDouble();

            if (newProbForBlock< _levelData.ProbabilityOfBlock && _blockCount <_levelData.MaxCountOfCloseRoad && !_preRowHasObstacle)
            {
                _blockCount++;
                _preRowHasObstacle = true;
                for (int j = 0; j < _levelData.LineCount; j++)
                {
                    var gO = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.Obstacle.ToString(),
                        levelHolder);
                    if (_levelData.LineCount %2==1)
                    {
                        gO.transform.position = new Vector3(-_scaleOfObstacle*((_levelData.LineCount-1)/2) +
                                                            _scaleOfObstacle*j , 0, _posCounter*_scaleOfObstacle);
                    }
                    else
                    {
                        gO.transform.position = new Vector3(-_scaleOfObstacle*((_levelData.LineCount)/2) +
                                                            _scaleOfObstacle*j , 0, _posCounter*_scaleOfObstacle);
                    }
                    _preRowHasObstacle = true;
                    _lineList.Add(gO);
                    _obstacleList.Add(gO);
                    CalculateObstacleText(true,gO);
                }
            }
            else 
            {
                var newProbForEmpty = (float)random.NextDouble();

                if (newProbForEmpty < _levelData.ProbabilityOfEmptyBlock && !_preRowHasObstacle)
                {
                    var gO = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.Obstacle.ToString(),
                        levelHolder);
                    gO.transform.position = new Vector3(-_scaleOfObstacle * ((_levelData.LineCount) / 2) +
                                                        Random.Range(0,_levelData.LineCount+1)*_scaleOfObstacle, 0, _posCounter * _scaleOfObstacle);
                    CalculateObstacleText(false,gO);
                    _lineList.Add(gO);
                    _obstacleList.Add(gO);
                    _preRowHasObstacle = true;
                }
                else
                {
                    var gO = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.Collectable.ToString(), transform);
                    gO.transform.position = new Vector3(-_scaleOfObstacle * ((_levelData.LineCount-1) / 2) +
                                                        Random.Range(0,_levelData.LineCount+1)*_scaleOfObstacle, 0, _posCounter * _scaleOfObstacle);
                    var score = Random.Range(1, _levelData.maxObstacleScore);
                    LevelSignals.Instance.onSetCollectableScore?.Invoke(score, gO);
                    _collectableList.Add(gO);
                    _preRowHasObstacle = false;
                }
            }
            _posCounter++;
            _lineList.Clear();
        }
    }
}