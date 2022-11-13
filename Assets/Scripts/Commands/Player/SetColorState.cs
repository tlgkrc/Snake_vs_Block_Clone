using System.Collections.Generic;
using UnityEngine;

namespace Commands.Player
{
    public class SetColorState
    {
        #region Self Variables
        #region Private Variables
        private List<GameObject> _stackList;
        #endregion
        #endregion

        public SetColorState(ref List<GameObject> stackList)
        {
            _stackList = stackList;
        }
    }
}