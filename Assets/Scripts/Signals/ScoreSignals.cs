using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction<int> onUpdateStackScore = delegate { };
        public UnityAction<int> onUpdateTotalScore = delegate {  };
        public UnityAction<GameObject> onSetLeadPosition = delegate {  };
    }
}