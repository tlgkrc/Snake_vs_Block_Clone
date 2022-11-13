using Enums;
using UnityEngine;
using Managers;
using Signals;


namespace Commands
{
    public class InitialzeStackCommand
    {
        #region Self Variables
        #region Private Variables
        private StackManager _manager;
        private GameObject _collectable;
        #endregion
        #endregion

        public InitialzeStackCommand(GameObject collectable,StackManager Manager)
        {
            _collectable = collectable;
            _manager = Manager;
        }
        public void Execute(int count)
        {
            for (int i = 0; i < count/*_manager.StackData.InitialStackItem*/; i++)
            {
                _collectable =
                    PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.Collected.ToString(), _manager.transform);
                _manager.ItemAddOnStack.Execute(_collectable);
            }
            ScoreSignals.Instance.onSetScore?.Invoke(count);
        }
    }
}