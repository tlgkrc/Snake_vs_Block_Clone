using System;

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
        public float RotateBorder;
        public float RunSpeed;
    }
}