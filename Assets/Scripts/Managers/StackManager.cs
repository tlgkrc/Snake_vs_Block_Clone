using System.Collections.Generic;
using Controllers;
using UnityEngine;
using Signals;
using Data.UnityObject;
using Data.ValueObject;
using Commands;
using Commands.Stack;
using DG.Tweening;
using Enums;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables
        
        #region Public Variables
        public List<GameObject> CollectedStackList = new List<GameObject>();
        public ItemAddOnStackCommand ItemAddOnStack;

        #endregion

        #region Seralized Veriables
        
        [SerializeField] private GameObject levelHolder;

        #endregion

        #region Private Variables

        private Vector3 _direction;
        private GameObject _playerGameObject;
        private StackData _stackData;
        private StackMoveController _stackMoveController;
        private ItemRemoveOnStackCommand _itemRemoveOnStackCommand;
        private ItemAddOnStackCommand _itemAddOnStackCommand;
        
        #endregion
        #endregion
        
        private void Awake()
        {
            _stackData = GetStackData();
            Init();
        }
        
        private StackData GetStackData() => Resources.Load<CD_Stack>("Data/CD_StackData").StackData;
        
        private void Init()
        {
            _stackMoveController = new StackMoveController();
            _stackMoveController.InisializedController(_stackData);
            _itemRemoveOnStackCommand = new ItemRemoveOnStackCommand(ref CollectedStackList, ref levelHolder);
            _itemAddOnStackCommand = new ItemAddOnStackCommand(ref CollectedStackList,transform,_stackData);
        }

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            StackSignals.Instance.onInteractionWithObstacle += _itemRemoveOnStackCommand.Execute;
            StackSignals.Instance.onPlayerGameObject += OnSetPlayer;
            StackSignals.Instance.onGetCurrentScore += OnGetStackCount;
            StackSignals.Instance.onInteractionWithPlayer += OnInteractionWithPlayer;
        }
        private void UnSubscribeEvent()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            StackSignals.Instance.onInteractionWithObstacle -= _itemRemoveOnStackCommand.Execute;
            StackSignals.Instance.onPlayerGameObject -= OnSetPlayer;
            StackSignals.Instance.onGetCurrentScore -= OnGetStackCount;
            StackSignals.Instance.onInteractionWithPlayer -= OnInteractionWithPlayer;
        }
        private void OnDisable()
        {
            UnSubscribeEvent();
        }
        #endregion

        private void Start()
        {
            ScoreSignals.Instance.onUpdateStackScore?.Invoke(CollectedStackList.Count);
        }
        
        private void Update()
        {
           StackMove();
        }

        private void OnSetPlayer(GameObject player)
        {
            _playerGameObject = player;
        }
        
        private void StackMove()
        {
             if (gameObject.transform.childCount > 0)
             {
                _stackMoveController.StackItemsMoveOrigin(_playerGameObject.transform.position, CollectedStackList);
             }
        }

        private void OnReset()
        {
            foreach (Transform childs in transform)
            {
                Destroy(childs.gameObject);
            }
            CollectedStackList.Clear();
        }

        private int OnGetStackCount()
        {
            return CollectedStackList.Count;
        }

        private void OnInteractionWithPlayer(int value)
        {
            for (int i = 0; i < value; i++)
            {
                var gO = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.Collected.ToString(), transform);
                _itemAddOnStackCommand.Execute(gO);
            }
            ScoreSignals.Instance.onUpdateStackScore?.Invoke(CollectedStackList.Count);
        }
    }
}