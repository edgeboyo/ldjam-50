using GameEvents;
using UnityEngine;

namespace Game.Core.GameEvents
{
    [CreateAssetMenu(fileName = "IntGameEvent", menuName = "ScriptableObjects/GameEvents/IntGameEvent")]
    public class IntGameEvent : ValueGameEvent<int>
    {
        
    }
}