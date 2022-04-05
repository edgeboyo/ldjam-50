using System;
using Core;
using Data;
using Helpers;
using Misc;
using Profiles;
using UnityEngine;

namespace Game
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected WeaponProfile profile;
        [SerializeField] protected Transform mainTr;
        [SerializeField] protected SpriteRenderer spriteRenderer;

        protected Direction _direction;
        protected Vector2 _directionVector;
        protected GameObject _originGo;
        protected bool _isDitched;
        protected Timer _lifetimeTimer;
        
        public WeaponData WeaponData => profile.WeaponData;

        protected float MovementSpeed => WeaponData.ProjectileSpeed;
        protected float RotationSpeed => WeaponData.ProjectileRotation;
        protected float Lifetime => WeaponData.ProjectileLifetime;

        private void Start()
        {
            _lifetimeTimer = new Timer(Lifetime);
            _lifetimeTimer.ChangeCallback(OnLifetimeEnded);
            _lifetimeTimer.Run();
        }

        private void Update()
        {
            if (_isDitched)
            {
                return;
            }
        }

        private void OnLifetimeEnded()
        {
            Ditch();
        }
        
        public void SetOrigin(GameObject originGo)
        {
            _originGo = originGo;
        }
        
        public void SetDirection(Direction direction)
        {
            var directionVector = VectorHelper.GetVectorFromDirection(direction);

            //Debug.Log(direction);

            _direction = direction;
            
            SetDirection(directionVector);
        }
        
        public void SetDirection(Vector2 directionVector)
        {
            _directionVector = directionVector;
            gameObject.transform.position = gameObject.transform.position + new Vector3(directionVector.x*3,3,0);

            // var angle = Vector2.SignedAngle(Vector2.up, directionVector);
            // var rotation = Quaternion.Euler(0f, 0f, angle);

            // mainTr.rotation = rotation;
        }

        public bool InteractsWith(GameObject go)
        {
            return !_isDitched && go != _originGo;
        }

        public abstract void Ditch(); // implemented on case-by case basis

        public abstract void Launch();
    }
}