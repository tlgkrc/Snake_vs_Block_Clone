using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Collectable", menuName = "Game/CD_Collectable", order = 0)]
    public class CD_Collectable : ScriptableObject
    {
        public CollectableData Data;
    }
}