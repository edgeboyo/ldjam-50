using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/Config/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public List<KeyCode> JumpKeys { get; private set; }
        [field: SerializeField] public List<KeyCode> AttackKeys { get; private set; }
    }
}