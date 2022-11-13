﻿using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Controllers
{
    public class StackMoveController
    {
        #region Self Variables

        #region Private Veriables

        private StackData _stackData;
        #endregion
        #endregion

        public void InisializedController(StackData stackdata)
        {
            _stackData = stackdata;
        }

        public void StackItemsMoveOrigin(Vector3 direction,List<GameObject> collectableStack)
        {
            if (collectableStack.Count <= 0)
            {
                return;
            }
            
            float directx = Mathf.Lerp(collectableStack[0].transform.localPosition.x, direction.x,_stackData.LerpSpeed_x);
            float directy = Mathf.Lerp(collectableStack[0].transform.localPosition.y, direction.y,_stackData.LerpSpeed_y);
            float directz = Mathf.Lerp(collectableStack[0].transform.localPosition.z, direction.z + _stackData.DistanceFormPlayer ,_stackData.LerpSpeed_z);
            
            collectableStack[0].transform.localPosition = new Vector3(directx, directy, directz);
            collectableStack[0].transform.LookAt(direction);
            StackItemsLerpMove(collectableStack);
        }

        public void StackItemsLerpMove(List<GameObject> _collectableStack)
        {
            for (int i = 1; i < _collectableStack.Count; i++)
            {
                Vector3 pos = _collectableStack[i - 1].transform.localPosition;
                pos.z = _collectableStack[i - 1].transform.localPosition.z - _stackData.CollectableOffsetInStack;
                _collectableStack[i].transform.localPosition = new Vector3(
                    Mathf.Lerp(_collectableStack[i].transform.localPosition.x, pos.x, _stackData.LerpSpeed_x),
                    Mathf.Lerp(_collectableStack[i].transform.localPosition.y, pos.y, _stackData.LerpSpeed_y),
                    Mathf.Lerp(_collectableStack[i].transform.localPosition.z, pos.z, _stackData.LerpSpeed_z));
                _collectableStack[i].transform.LookAt(_collectableStack[i-1].transform);
            }
        }
        
        public void StackItemsLerpMoveOnDronePool(List<GameObject> _collectableStack)
        {
            for (int i = 1; i < _collectableStack.Count; i++)
            {
                Vector3 pos = _collectableStack[i - 1].transform.localPosition;
                pos.z = _collectableStack[i - 1].transform.localPosition.z - _stackData.CollectableOffsetInStack;
                _collectableStack[i].transform.localPosition = new Vector3(
                    Mathf.Lerp(_collectableStack[i].transform.localPosition.x, pos.x, _stackData.LerpSpeed_x),
                    Mathf.Lerp(_collectableStack[i].transform.localPosition.y, pos.y, _stackData.LerpSpeed_y),
                    Mathf.Lerp(_collectableStack[i].transform.localPosition.z, pos.z, _stackData.LerpSpeed_z));
            }
        }
    }
}