using Data;
using UnityEngine;

namespace Profiles
{
    [CreateAssetMenu(fileName = "WeaponProfile", menuName = "ScriptableObjects/Profiles/WeaponProfile")]
    public class WeaponProfile : ScriptableObject
    {
        [field: SerializeField] public WeaponData WeaponData { get; private set; }
    }
}