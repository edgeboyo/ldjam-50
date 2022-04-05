using System;
using System.Collections.Generic;
using System.Linq;
using Context;
using Game.Core.GameEvents;
using UnityEngine;
using Zenject;

namespace UI
{
    public class HealthBarUI : MonoBehaviour
    {
        [Inject] private GlobalEvents _globalEvents;
        [Inject] private DiContainer _container;
        
        [SerializeField] private GameObject template;
        [SerializeField] private Transform contentParent;

        private List<GameObject> _instances = new List<GameObject>();
        
        private FloatGameEvent PlayerHealthChangedEvent => _globalEvents.PlayerHealthChanged;

        private void OnEnable()
        {
            PlayerHealthChangedEvent.RegisterListener(this, OnPlayerHealthChanged);
        }

        private void OnDisable()
        {
            PlayerHealthChangedEvent.UnregisterListener(this);
        }

        private void Awake()
        {
            template.SetActive(false);
        }

        private void OnPlayerHealthChanged(float health)
        {
            _instances.ToList().ForEach(Destroy);

            var healthInt = Mathf.RoundToInt(health);

            for (int i = 0; i < healthInt; i++)
            {
                var instance = _container.InstantiatePrefab(template, contentParent);
                
                instance.SetActive(true);
                
                // instance.transform.SetParent(contentParent);
                
                _instances.Add(instance);
            }
        }
    }
}