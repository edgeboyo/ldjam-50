using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Misc;
using Profiles;
using RuntimeSets;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game
{
    public class DynamicMusic : MonoBehaviour
    {
        private static DynamicMusic _instance = null;
        public static DynamicMusic Instance { get { return _instance; } }

        public AudioClip[] DrumsIntro;
        public AudioClip[] DrumsLoop;
        public AudioClip[] DrumsOutro;

        private int _bpm = 125;
        private bool sexxxx = true;
        private bool sexxxxx = false;

        private AudioSource _audioSource;

        public void Stop() 
        {
            var outro = DrumsOutro[0];
            var time = _audioSource.time;
            var timePerNote = 60.0 / _bpm / 4.0;
            _audioSource.Play();
        }
        
        void Start()
        {
            if (_instance != null)
            {
                Destroy(this);
            }
            else
            {
                _instance = this;
                _audioSource = GetComponents<AudioSource>()[1];
                _audioSource.clip = DrumsIntro[0];
                _audioSource.Play();
                sexxxx = true;
                sexxxxx = false;
            }
        }

        void Dead()
        {
            _audioSource = GetComponents<AudioSource>()[1];
            _audioSource.loop = false;
            sexxxx = false;
        }

        public static void ImDead()
        {
            _instance.Dead();
        }

        void Update()
        {
            _audioSource = GetComponents<AudioSource>()[1];

            if (!_audioSource.isPlaying)
            {
                if (sexxxx)
                {
                    _audioSource.loop = true;
                    _audioSource.clip = DrumsLoop[0];
                    _audioSource.Play();
                } else
                {
                    if (sexxxxx == false)
                    {
                        _audioSource.loop = false;
                        _audioSource.clip = DrumsOutro[0];
                        _audioSource.Play();
                        sexxxxx = true;
                    } else
                    {
                        Destroy(_instance);
                        _instance = null;
                    }
                }
            }
        }
    }
}