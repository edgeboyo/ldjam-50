using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Game
{
    public abstract class CharacterBody : MonoBehaviour
    {
        [SerializeField] private CharacterBehaviour playerController;
        [SerializeField] private CharacterMovement characterMovement;
        [SerializeField] private List<Animator> animators;

        private Direction _currentDirection = Direction.Right;
        
        private const string SpeedAnimationName = "Speed";
        private const string JumpAnimationName = "IsJumping";
        private const string AttackAnimationName = "IsAttacking";

        public Direction CurrentDirection => _currentDirection;
        
        protected virtual void SetFlip(Direction direction)
        {
            _currentDirection = direction;
        }

        private void OnEnable()
        {
            characterMovement.Jumped += OnJumped;
            characterMovement.Grounded += OnGrounded;
            playerController.TriggerAttack += OnAttack;
            playerController.CooldownOver += OnWeaponCoolDownOver;
        }

        private void OnDisable()
        {
            characterMovement.Jumped -= OnJumped;
            characterMovement.Grounded -= OnGrounded;
            playerController.TriggerAttack -= OnAttack;
            playerController.CooldownOver -= OnWeaponCoolDownOver;
        }

        private void Update()
        {
            if (characterMovement.IsMovingRight)
            {
                SetAnimatorFloat(SpeedAnimationName, characterMovement.AbsoluteHorizontalVelocity);
                
                SetFlip(Direction.Right);
            }
            else if (characterMovement.IsMovingLeft)
            {
                SetAnimatorFloat(SpeedAnimationName, characterMovement.AbsoluteHorizontalVelocity);
                
                SetFlip(Direction.Left);
            }
            else
            {
                SetAnimatorFloat(SpeedAnimationName, 0);
            }
        }

        private void OnJumped()
        {
            SetAnimatorBool(JumpAnimationName, true);
        }

        private void OnGrounded()
        {
            SetAnimatorBool(JumpAnimationName, false);
        }

        private void OnAttack()
        {
            SetAnimatorBool(AttackAnimationName, true);
        }

        private void OnWeaponCoolDownOver()
        {
            SetAnimatorBool(AttackAnimationName, false);
        }

        private void SetAnimatorBool(string flagName, bool value)
        {
            foreach(var anim in animators)
            {
                anim.SetBool(flagName, value);
            }
        }

        private void SetAnimatorFloat(string flagName, float value)
        {
            foreach(var anim in animators)
            {
                anim.SetFloat(flagName, value);
            }
        }
    }
}