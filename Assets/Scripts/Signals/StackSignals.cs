using System;
using UnityEngine;
using UnityEngine.Events;
using Extentions;

namespace Signals
{
    public class StackSignals: MonoSingleton<StackSignals>
    {
       // public UnityAction<GameObject> onInteractionObstacle = delegate { };
        public UnityAction<GameObject> onPlayerGameObject = delegate { };
        public UnityAction<bool> onLastCollectableAddedToPlayer = delegate {  };
        public Func<int> onGetCurrentScore = delegate { return 1; };
        public UnityAction<int> onInteractionWithPlayer = delegate {  };
        public UnityAction onInteractionWithObstacle = delegate {  };

    }
}