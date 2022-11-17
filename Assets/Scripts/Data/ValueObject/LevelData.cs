using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class LevelData
    {
        [PropertyOrder(1)]
        public int MaxCubeOnALine;
        public int LineCount;
        public int IncreasingAmount;
        [PropertyRange(0,"MaxCubeOnALine")]
        public int MaxCountOfCloseRoad;
        [Range(0,1)]
        public float ProbabilityOfBlock;
        public GameObject Obstacle;
        public Color RoadColor;
        public GameObject Collectable;
        public int maxObstacleScore;
        [Range(0,1)]
        public float ProbabilityOfEmptyBlock;
    }
}