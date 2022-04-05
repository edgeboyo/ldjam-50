using System;
using Core;
using Data;
using Helpers;
using Misc;
using Profiles;
using UnityEngine;

namespace Game
{
    public class SwordScript : Projectile
    {

        private void Update()
        {
            if (_isDitched)
            {
                return;
            }

            _lifetimeTimer.Advance(Time.deltaTime);

            Move();
        }

        public override void Ditch()
        {
            _isDitched = true;

            gameObject.SetActive(false);

            Destroy(gameObject, 2f);
        }

        private void Move()
        {
            var move = MovementSpeed * Time.deltaTime * _directionVector;

            mainTr.Translate(move, Space.World);
        }

        public override void Launch()
        {
            if (_direction == Direction.Left)
            {
                spriteRenderer.flipX = true;
            }
        }
    }
}