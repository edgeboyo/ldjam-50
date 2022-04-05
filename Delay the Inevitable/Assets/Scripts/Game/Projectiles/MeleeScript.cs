using System;
using Core;
using Data;
using Helpers;
using Misc;
using Profiles;
using UnityEngine;

namespace Game
{
    public class MeleeScript : Projectile
    { 
        private void Update()
        {
            //Debug.Log("Lego");
            if (_isDitched)
            {
                return;
            }

            _lifetimeTimer.Advance(Time.deltaTime);
        }

        public override void Ditch()
        {
            _isDitched = true;
            Destroy(gameObject);
        }


        public override void Launch()
        {
            Collider2D coll = _originGo.GetComponent<Collider2D>();

            BoxCollider2D myCollider = GetComponent<BoxCollider2D>();

            myCollider.offset = coll.offset;
            myCollider.size = coll.bounds.size;
        }
    }
}