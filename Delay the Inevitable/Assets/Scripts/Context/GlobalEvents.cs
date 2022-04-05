using Game.Core.GameEvents;
using UnityEngine;

namespace Context
{
    [CreateAssetMenu(fileName = "GlobalEvents", menuName = "ScriptableObjects/Context/GlobalEvents")]
    public class GlobalEvents : ScriptableObject
    {
        [field: SerializeField] public FloatGameEvent PlayerHealthChanged;
    }
}