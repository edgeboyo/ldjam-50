using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class MovementData
    {
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
        [field: SerializeField] public float JumpGravityMultiplier { get; private set; }
        [field: SerializeField] public float FallGravityMultiplier { get; private set; }
        [field: SerializeField] public float FlightModifier { get; private set; }
    }
}