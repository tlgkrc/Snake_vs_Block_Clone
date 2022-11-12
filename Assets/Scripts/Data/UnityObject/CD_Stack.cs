﻿using UnityEditor;
using UnityEngine;
using Data.ValueObject;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_StackData", menuName = "Game/CD_Stack", order = 0)]
    public class CD_Stack : ScriptableObject
    {
        public StackData StackData;
    }
}