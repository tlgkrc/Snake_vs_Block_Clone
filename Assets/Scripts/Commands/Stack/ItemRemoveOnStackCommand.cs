using System.Collections.Generic;
using Enums;
using Signals;
using UnityEngine;

namespace Commands.Stack
{
    public class ItemRemoveOnStackCommand
    {
        #region Self Variables
        #region Private Variables
        private List<GameObject> _collectableStack;
        private GameObject _levelHolder;
        #endregion
        #endregion
        
        public ItemRemoveOnStackCommand(ref List<GameObject> CollectableStack,ref GameObject levelHolder)
        {
            _collectableStack = CollectableStack;
            _levelHolder = levelHolder;
        }
        public void Execute()
        {
            PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolTypes.Collected.ToString(), _collectableStack[^1].gameObject);
            _collectableStack.RemoveAt(_collectableStack.Count-1);
            _collectableStack.TrimExcess();
            ScoreSignals.Instance.onUpdateStackScore?.Invoke(_collectableStack.Count);
            if (_collectableStack.Count<=0)
            {
                LevelSignals.Instance.onLevelFailed?.Invoke();
            }
        }
    }
}