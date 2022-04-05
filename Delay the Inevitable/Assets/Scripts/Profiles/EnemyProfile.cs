using Data;
using UnityEngine;

namespace Profiles
{
    [CreateAssetMenu(fileName = "EnemyProfile", menuName = "ScriptableObjects/Profiles/EnemyProfile")]
    public class EnemyProfile : ScriptableObject
    {
        [field: SerializeField] public MovementData MovementData { get; private set; }
        [field: SerializeField] public LifeData LifeData { get; private set; }

        [field: SerializeField] public WeaponProfile WeaponProfile { get; private set; }
    }
}