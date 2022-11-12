using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public class DublicateStateItemsCommand
    {
        #region Self Variables

        #region Private Variables
        private List<GameObject> _collectableStack;
        private ItemAddOnStackCommand _addOnStackCommand;
        private GameObject _gameObject;
        #endregion
        #endregion

        public DublicateStateItemsCommand(ref List<GameObject> collectableStack, ref ItemAddOnStackCommand addOnStackCommand)
        {
            _collectableStack = collectableStack;
            _addOnStackCommand = addOnStackCommand;
        }

        public void Execute(int value)
        {
            for (int i = 0; i < value; i++)
            {
                _gameObject = GameObject.Instantiate(_collectableStack[value-1].gameObject);//use pool
                _addOnStackCommand.Execute(_gameObject);
            }
        }
    }
}