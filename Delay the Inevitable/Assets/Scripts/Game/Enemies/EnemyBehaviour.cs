using System.Linq;
using Data;
using Profiles;
using RuntimeSets;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
using System;
using System.Collections.Generic;
using Misc;

namespace Game
{
    public class EnemyBehaviour : CharacterBehaviour
    {
        [Inject] private EnemyRuntimeSet _enemyRuntimeSet;
        [Inject] private PlayerRuntimeSet _playerRuntimeSet;
        [Inject] private DiContainer _container;

        [SerializeField] private EnemyProfile profile;

        private WeaponData WeaponData => profile.WeaponProfile.WeaponData;

        private GameObject ProjectileTemplate => WeaponData.ProjectileTemplate;

        private const float StopDistance = 13f;
        
        protected override MovementData MovementData => profile.MovementData;
        protected override LifeData LifeData => profile.LifeData;
      
        private PlayerController Player => _playerRuntimeSet == null ? GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>() : _playerRuntimeSet.Items.FirstOrDefault();
        private bool PlayerExists => Player;

        private Rigidbody2D rb2D;

        public override event Action TriggerAttack;

        public override event Action CooldownOver;

        protected bool SuppressDefaultAttack = true;

        // Attack tools

        private AudioClip[] audioClips => WeaponData.AudioClips;

        public List<GameObject> Projectiles = new List<GameObject>();

        private const string ProjectileLayer = "EnemyProjectile";

        private Timer _cooldownTimer;

        private float WeaponCooldown => WeaponData.Cooldown;

        private void OnEnable()
        {
            if (_enemyRuntimeSet != null)
            {
                _enemyRuntimeSet.Add(this);
            }
        }

        private void OnDisable()
        {
            if (_enemyRuntimeSet != null)
            {
                _enemyRuntimeSet.Remove(this);
            }  
        }

        protected virtual void Start()
        {
            _cooldownTimer = new Timer();
        }

        protected virtual void Update()
        {
            // ENEMY BEHAVIOUR HERE!

            _cooldownTimer.Advance(Time.deltaTime);
            
            if (!PlayerExists)
            {
                return;
            }

            var vectorToPlayer = Player.Position - Position;
            var distanceToPlayer = vectorToPlayer.magnitude;
            
            var reachedDistance = distanceToPlayer <= StopDistance;

            // Attack
            if (!SuppressDefaultAttack)
                Attack();

            if (reachedDistance)
            {
                return;
            }

            // Jump

            if (rb2D == null){
                rb2D = gameObject.GetComponent<Rigidbody2D>();
            }

            int jumpChoice = Random.Range(0, 151);
            if(jumpChoice == 50){
                gameObject.GetComponent<CharacterMovement>().Jump(1);
            } else if(rb2D != null){
                if(rb2D.velocity.x < 0.1f){
                    jumpChoice = Random.Range(0, 10);
                    if(jumpChoice == 1){
                        gameObject.GetComponent<CharacterMovement>().Jump(1);
                    }
                }
            }
            
            var horizontalInput = vectorToPlayer.x > 0 ? 1f : -1f;
            
            Movement.MoveHorizontally(horizontalInput);
        }

        protected virtual void Attack()
        {
            ThrowProjectile();
        }

        protected void ThrowProjectile()
        {
            if (_cooldownTimer.IsRunning && !_cooldownTimer.GoalTimeReached)
            {
                return;
            }

            List<GameObject> cleanedProjectiles = new List<GameObject>();
            foreach (var proj in Projectiles)
            {
                if (proj.gameObject != null && proj.gameObject.activeInHierarchy)
                {
                    cleanedProjectiles.Add(proj);
                }

            }

            Projectiles = cleanedProjectiles;

            if (Projectiles.Count >= WeaponData.MaxProjectiles)
            {
                return;
            }

            TriggerAttack?.Invoke();


            GameObject instance = null;
            if (_container == null)
            {
                instance = Instantiate(ProjectileTemplate, gameObject.transform.position, Quaternion.identity);
            } else
            {
               instance = _container.InstantiatePrefab(ProjectileTemplate);
            }

            var instanceTr = instance.transform;


            instanceTr.position = Position;

            instance.layer = LayerMask.NameToLayer(ProjectileLayer);

            Projectiles.Add(instance);

            Projectile projectile = instance.GetComponent<Projectile>();

            if (projectile != null)
            {
                var currentDirection = Body.CurrentDirection;

                projectile.SetOrigin(gameObject);
                projectile.SetDirection(currentDirection);
                projectile.Launch();
            }

            _cooldownTimer.ChangeGoalTime(WeaponCooldown);
            _cooldownTimer.Reset();

            AudioSource audioSource = GetComponent<AudioSource>();

            if (audioClips.Length != 0 && audioSource != null)
            {
                audioSource.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
                audioSource.Play(0);
            }
            else if(audioSource == null)
            {
                Debug.LogWarning("Enemy " + gameObject.name + " does not have an audio source component...");
            }
            else
            {
                Debug.LogWarning("No audio set for " + instance.name);
            }
        }
    }
}