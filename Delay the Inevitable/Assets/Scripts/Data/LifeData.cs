using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class LifeData
    {
        [field: SerializeField] public float HealthPoints { get; private set; }
        [field: SerializeField] public GameObject BloodBox { get; private set; }
    }
}