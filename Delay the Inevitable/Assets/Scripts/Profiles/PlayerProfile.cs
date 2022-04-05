using Data;
using UnityEngine;

namespace Profiles
{
    [CreateAssetMenu(fileName = "PlayerProfile", menuName = "ScriptableObjects/Profiles/PlayerProfile")]
    public class PlayerProfile : ScriptableObject
    {
        [field: SerializeField] public MovementData MovementData { get; private set; }
        [field: SerializeField] public LifeData LifeData { get; private set; }
        [field: SerializeField] public WeaponProfile DefaultWeaponProfile { get; private set; }
    }
}