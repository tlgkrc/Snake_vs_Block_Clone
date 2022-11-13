using System.Collections.Generic;
using UnityEngine;
using Commands;
using Commands.Stack;
using Signals;

namespace Commands
{
    public class UnstackItemsToStack
    {
        #region Self Variables
        #region Private Variables
        private List<GameObject> _collectableStack;
        private List<GameObject> _unStackList;
        private GameObject _stackManager;
        #endregion
        #endregion
        
        public UnstackItemsToStack(ref List<GameObject> collectableStack,ref List<GameObject> unStackList,GameObject stackManager)
        {
            _collectableStack = collectableStack;
            _unStackList = unStackList;
            _stackManager = stackManager;
        }
        public void Execute()
        {
            foreach (var i in _unStackList)
            {
                i.transform.SetParent(_stackManager.transform);
                _collectableStack.Add(i);
            }
            
            _unStackList.Clear();
            ScoreSignals.Instance.onSetScore?.Invoke(_collectableStack.Count);
            ScoreSignals.Instance.onSetLeadPosition?.Invoke(_collectableStack[0]);

       
        }
    }
}