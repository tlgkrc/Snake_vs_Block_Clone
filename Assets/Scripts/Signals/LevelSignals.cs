using System;
using Extentions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class LevelSignals : MonoSingleton<LevelSignals>
    {
        public UnityAction onLevelInitialize = delegate { };
        public UnityAction onClearActiveLevel = delegate { };
        public UnityAction onLevelFailed = delegate { };
        public UnityAction onLevelSuccessful = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onRestartLevel = delegate { };
        public UnityAction<Vector3> onNextLevelInitialize =delegate {  };
        public UnityAction<int,GameObject> onSetObstacleScore = delegate {  };
        public UnityAction<int,GameObject> onSetCollectableScore = delegate {  };
        public Func<Vector3> onGetPlayerPos = delegate { return Vector3.zero;};
    }
}