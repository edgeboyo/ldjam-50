using GameEvents;
using UnityEngine;

namespace Game.Core.GameEvents
{
    [CreateAssetMenu(fileName = "FloatGameEvent", menuName = "ScriptableObjects/GameEvents/FloatGameEvent")]
    public class FloatGameEvent : ValueGameEvent<float>
    {
        
    }
}