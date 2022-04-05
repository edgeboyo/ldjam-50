using Misc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class SkeleBehaviour : EnemyBehaviour
{
        [SerializeField] private float ThrowRangeLowerBound = 3f;
        [SerializeField] private float ThrowRangeUpperBound = 6f;

        [SerializeField] private float ScanLength = 20f;

        private Timer RandomTimer; 

        protected override void Start()
        {
            base.Start();
            SuppressDefaultAttack = true;
            RandomTimer = new Timer();
        }

        protected override void Update()
        {
            base.Update();
            RandomTimer.Advance(Time.deltaTime);

            RaycastHit2D leftCast = Physics2D.Raycast(transform.position, Vector2.left, ScanLength, LayerMask.GetMask("Player"));
            RaycastHit2D rightCast = Physics2D.Raycast(transform.position, Vector2.right, ScanLength, LayerMask.GetMask("Player"));

            if(leftCast || rightCast )
            {
                Attack();
            }
        }

        protected override void Attack()
        {
            if (RandomTimer.IsRunning && !RandomTimer.GoalTimeReached)
            {
                return;
            }

            ThrowProjectile();

            RandomTimer.ChangeGoalTime(Random.Range(ThrowRangeLowerBound, ThrowRangeUpperBound));
            RandomTimer.Reset();
        }
    }
}
