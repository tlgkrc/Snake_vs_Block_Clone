using System.Collections.Generic;
using Data.ValueObject;
using Signals;
using UnityEngine;

namespace Commands.Stack
{
    public class ItemAddOnStackCommand
    {
        #region Self Variables
        #region Private Variables
        private List<GameObject> _collectableStack;
        private Transform _transform;
        private StackData _stackData;
        #endregion
        #endregion
        
        public ItemAddOnStackCommand(ref List<GameObject> collectableStack,Transform transform,StackData stackData)
        {
            _collectableStack = collectableStack;
            _transform = transform;
            _stackData = stackData;
        }
        
        public void Execute(GameObject newGameObject)
        {
            if (newGameObject==null)
            {
                return;
            }
            if (_collectableStack.Count == 0)
            {
                _collectableStack.Add(newGameObject);
                newGameObject.transform.SetParent(_transform);
                newGameObject.transform.localPosition = Vector3.zero;
            }
            else
            {
                newGameObject.transform.SetParent(_transform);
                Vector3 newPos = _collectableStack[^1].transform.localPosition;
                newPos.z -= _stackData.CollectableOffsetInStack;
                newGameObject.transform.localPosition = newPos;
                _collectableStack.Add(newGameObject);
            }
            ScoreSignals.Instance.onUpdateStackScore?.Invoke(_collectableStack.Count);
        }
    }
}