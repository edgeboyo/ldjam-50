using Misc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class ZombieBehaviour : EnemyBehaviour
{
        protected override void Start()
        {
            base.Start();
            SuppressDefaultAttack = false;
        }
    }
}
