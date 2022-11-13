using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class PlayerData
    {
        public PlayerMovementData MovementData;
    }

    [Serializable]
    public class PlayerMovementData
    {
        public float ForwardSpeed;
        public float SidewaysSpeed;
        public float RunSpeed;
        public Vector2 ClampRotation;
        public float Damping;
    }
}