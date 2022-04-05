using System;
using Data;
using Extensions;
using Misc;
using UnityEngine;
using Zenject;

namespace Game
{
    public class CharacterHealth : MonoBehaviour
    {
        private float _healthPoints;
        private bool _isDead;

        public LifeData LifeData { get; set; }

        private float MaxHealthPoints => LifeData.HealthPoints;

        public float CurrentHealthPoints => _healthPoints;

        public event Action<float> HealthChanged;

        protected GameObject bloodBox;

        public GameObject playerDeathFader;

        public GameObject resetButton;

        private void Start()
        {
            _healthPoints = MaxHealthPoints;
            bloodBox = LifeData.BloodBox;
        }

        public void ReceiveDamage(float damage)
        {
            _healthPoints -= damage;

            var died = Mathf.Approximately(_healthPoints, 0f) || _healthPoints < 0;
            
            if (died)
            {
                _isDead = true;
                
                if(gameObject.CompareTag("Player"))
                {
                    DynamicMusic.ImDead();
                    gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

                    GameObject fader = Instantiate(playerDeathFader, gameObject.transform.position, Quaternion.identity);
                    fader.GetComponent<FadeInOut>().totalTime = 2f;
                    fader.GetComponent<FadeInOut>().DoFadeIn();

                    fader.transform.localScale *= 100;

                    fader.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    fader.GetComponent<SpriteRenderer>().sortingLayerName = "UI";

                    gameObject.GetComponent<TwoPieceCharacterBody>().upperSR.sortingOrder = fader.GetComponent<SpriteRenderer>().sortingOrder + 1;
                    gameObject.GetComponent<TwoPieceCharacterBody>().upperSR.sortingLayerName = "UI";

                    gameObject.GetComponent<TwoPieceCharacterBody>().lowerSR.sortingOrder = fader.GetComponent<SpriteRenderer>().sortingOrder + 1;
                    gameObject.GetComponent<TwoPieceCharacterBody>().lowerSR.sortingLayerName = "UI";

                    gameObject.GetComponent<PlayerController>().enabled = false;

                    GameObject resetButtonInstance = Instantiate(resetButton, gameObject.transform.position + new Vector3(0, -35, 0), Quaternion.identity);

                    // resetButtonInstance.transform.parent = gameObject.transform;

                    resetButtonInstance.transform.localScale *= 20;

                    resetButtonInstance.GetComponent<SpriteRenderer>().sortingOrder = 5;

                    FadeInOut resetFader = resetButtonInstance.AddComponent<FadeInOut>();

                    resetFader.totalTime = 4f;

                    resetFader.DoFadeIn();
                }
                else 
                {
                    gameObject.SetActive(false); // temp
                }

                
            }
            
            HealthChanged?.Invoke(_healthPoints);

            if(bloodBox != null){
                Instantiate(bloodBox, gameObject.transform.position, new Quaternion(0, 0, 0, 1));
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isDead)
            {
                return;
            }
            
            var otherGo = other.gameObject;

            if (otherGo.transform.TryGetComponentRecursive(out Projectile projectile))
            {
                if (!projectile.InteractsWith(gameObject))
                {
                    return;
                }
                
                projectile.Ditch();
                
                var weaponData = projectile.WeaponData;
                var damage = weaponData.Damage;
                
                ReceiveDamage(damage);
            }
        }
    }
}