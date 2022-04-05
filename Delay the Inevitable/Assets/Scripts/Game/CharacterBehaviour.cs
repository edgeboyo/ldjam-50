using Data;
using UnityEngine;
using System;

namespace Game
{
    public abstract class CharacterBehaviour : MonoBehaviour
    {
        [SerializeField] private CharacterMovement characterMovement;
        [SerializeField] private CharacterHealth characterHealth;
        [SerializeField] private CharacterBody characterBody;

        protected CharacterMovement Movement => characterMovement;
        protected CharacterHealth Health => characterHealth;
        protected CharacterBody Body => characterBody;
        
        protected abstract MovementData MovementData { get; }
        protected abstract LifeData LifeData { get; }

        public abstract event Action TriggerAttack;

        public abstract event Action CooldownOver;

        public virtual Vector2 Position => transform.position;

        protected virtual void Awake()
        {
            characterMovement.MovementData = MovementData;
            
            characterHealth.LifeData = LifeData;
        }
    }
}