using System;
using System.Collections.Generic;
using System.Linq;
using Config;
using Context;
using Data;
using Extensions;
using Game.Core.GameEvents;
using Misc;
using Profiles;
using RuntimeSets;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Random = System.Random;

namespace Game
{
    public class PlayerController : CharacterBehaviour
    {
        [Inject] private PlayerConfig _playerConfig;
        [Inject] private PlayerRuntimeSet _playerRuntimeSet;
        [Inject] private DiContainer _container;
        [Inject] private GlobalEvents _globalEvents;

        [SerializeField] private PlayerProfile profile;

        private WeaponProfile _equippedWeaponProfile;
        private Timer _cooldownTimer;
        
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";

        private const string ProjectileLayer = "PlayerProjectile";

        protected override MovementData MovementData => profile.MovementData;
        protected override LifeData LifeData => profile.LifeData;

        private FloatGameEvent HealthChangedEvent => _globalEvents.PlayerHealthChanged;
        
        private WeaponData WeaponData => HasEquippedWeapon 
            ? _equippedWeaponProfile.WeaponData 
            : profile.DefaultWeaponProfile.WeaponData;

        private List<KeyCode> JumpKeys => _playerConfig.JumpKeys;
        private List<KeyCode> AttackKeys => _playerConfig.AttackKeys;
        private bool HasEquippedWeapon => _equippedWeaponProfile;
        private float WeaponCooldown => WeaponData.Cooldown;
        private GameObject ProjectileTemplate => WeaponData.ProjectileTemplate;
        
        private AudioClip[] audioClips => WeaponData.AudioClips;

        public  List<GameObject> Projectiles = new List<GameObject>();

        public override event Action TriggerAttack;

        public override event Action CooldownOver;

        public int currentLevel;

        public GameObject SceneMover;

        private void OnEnable()
        {
            _playerRuntimeSet.Add(this);
            
            Health.HealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            _playerRuntimeSet.Remove(this);
            
            Health.HealthChanged -= OnHealthChanged;
        }

        private void Start()
        {
            _cooldownTimer = new Timer();
            
            HealthChangedEvent.Raise(Health.CurrentHealthPoints);
        }

        private void Update()
        {
            _cooldownTimer.Advance(Time.deltaTime);
            
            if (JumpKeys.Any(Input.GetKeyDown))
            {
                var verticalInput = Input.GetAxisRaw(VerticalAxis);
                Movement.Jump(verticalInput);
            }
        
            var horizontalInput = Input.GetAxisRaw(HorizontalAxis);

            Movement.MoveHorizontally(horizontalInput);

            if (_cooldownTimer.IsRunning && !_cooldownTimer.GoalTimeReached)
            {
                return;
            }

            CooldownOver?.Invoke();

            if (AttackKeys.Any(Input.GetKeyDown))
            {
                Attack();
            }
        }
        
        private void OnHealthChanged(float health)
        {
            HealthChangedEvent.Raise(health);
        }

        private void Attack()
        {

            List<GameObject> cleanedProjectiles = new List<GameObject>();
            foreach(var proj in Projectiles)
            {
                if(proj.gameObject != null && proj.gameObject.activeInHierarchy)
                {
                    cleanedProjectiles.Add(proj);
                }
                
            }

            Projectiles = cleanedProjectiles;

            if(Projectiles.Count >= WeaponData.MaxProjectiles)
            {
                return;
            }

            TriggerAttack?.Invoke();

            var instance = _container.InstantiatePrefab(ProjectileTemplate);
            var instanceTr = instance.transform;
            

            instanceTr.position = Position;

            Debug.Log(LayerMask.NameToLayer(ProjectileLayer));

            instance.layer = LayerMask.NameToLayer(ProjectileLayer);

            Projectiles.Add(instance);

            if (instance.transform.TryGetComponentRecursive(out Projectile projectile))
            {
                var currentDirection = Body.CurrentDirection;

                projectile.SetOrigin(gameObject);
                projectile.SetDirection(currentDirection);
                projectile.Launch();
            }
            
            _cooldownTimer.ChangeGoalTime(WeaponCooldown);
            _cooldownTimer.Reset();

            if(audioClips.Length != 0)
            {
                var audioSource = GetComponent<AudioSource>();
                audioSource.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
                audioSource.Play(0);
            }
            else
            {
                Debug.LogWarning("No audio set for " + instance.name);
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Player entered " + other.name);
            if (other.gameObject.CompareTag("Portal"))
            {
                DontDestroyOnLoad(gameObject);

                currentLevel++;

                Instantiate(SceneMover, gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}
