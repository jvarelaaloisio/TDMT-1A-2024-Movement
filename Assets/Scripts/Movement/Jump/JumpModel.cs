using System;
using UnityEngine;

namespace Movement.Jump
{
    [Serializable]
    public class JumpModel
    {
        [field: SerializeField] public float Force { get; private set; } = 10;
        [field: SerializeField] public int MaxQty { get; private set; } = 2;
        [field: SerializeField] public float FloorAngle { get; private set; } = 30;
    }
}