using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class WeaponData
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public GameObject ProjectileTemplate { get; private set; }
        [field: SerializeField] public float ProjectileSpeed { get; private set; }
        [field: SerializeField] public float ProjectileRotation { get; private set; }
        [field: SerializeField] public float ProjectileLifetime { get; private set; }
        [field: SerializeField] public int MaxProjectiles { get; private set; }
        [field: SerializeField] public AudioClip[] AudioClips { get; private set; }

        [field: SerializeField] public GameObject[] SuplementalScripts { get; private set; } // gameobjects that would get attached to the player/enemy to display weapons etc
    }
}