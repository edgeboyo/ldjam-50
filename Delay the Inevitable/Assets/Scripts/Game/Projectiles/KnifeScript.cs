using System;
using Core;
using Data;
using Helpers;
using Misc;
using Profiles;
using UnityEngine;

namespace Game
{
    public class KnifeScript : Projectile
    {
        private void Update()
        {
            if (_isDitched)
            {
                return;
            }

            _lifetimeTimer.Advance(Time.deltaTime);

            Move();
            Rotate();
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

        private void Rotate()
        {
            var angle = RotationSpeed * Time.deltaTime;

            if (_direction == Direction.Left)
            {
                angle *= -1f;
            }

            mainTr.Rotate(0f, 0f, angle);
        }

        public override void Launch()
        {
            // nothing here
        }
    }
}