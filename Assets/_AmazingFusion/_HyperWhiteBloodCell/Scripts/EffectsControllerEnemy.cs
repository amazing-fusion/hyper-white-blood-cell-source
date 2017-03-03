﻿using MovementEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class EffectsControllerEnemy : OptimizedBehaviour
    {
        [SerializeField]
        DamageController _damageController;

        [SerializeField]
        SpriteRenderer _spriteEnemy;

        [SerializeField]
        AnimatorControllerEnemy _animator;

        [SerializeField]
        ParticleSystem _explosionParticles;

        [SerializeField]
        float _magnitudeShake;

        [SerializeField]
        float _roughnessShake;

        [SerializeField]
        float _fadeInTimeShake;

        [SerializeField]
        float _fadeOutTimeShake;

        [SerializeField]
        Transform _scaleEffect;

        void Awake()
        {
            _damageController.OnDie += EffectsDiedEnemy;
            _damageController.OnTakeDamage += EffectScaleDamage;
        }
        
        void Start()
        {
            _explosionParticles.Stop();
            _scaleEffect.localScale = Vector3.zero;
        }

        public void EffectScaleDamage()
        {
            //_scaleEffect.localRotation = GameController.Instance.PlayerChild.localRotation;
            _scaleEffect.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 180));
            _scaleEffect.GetComponent<SequenceEasingAnimation>().Play();
        }

        public void EffectsDiedEnemy(System.Action action)
        {
            Timing.RunCoroutine(DoEffectsDiedEnemy(action));
        }
        
        IEnumerator<float> DoEffectsDiedEnemy(System.Action action)
        {
            _explosionParticles.Play();
            _spriteEnemy.enabled = false;
            AnimatorControllerEnemy.Instance.AnimationDiedEnemy();
            EZCameraShake.CameraShaker.Instance.ShakeOnce
                (_magnitudeShake, _roughnessShake, _fadeInTimeShake, _fadeOutTimeShake);

            yield return Timing.WaitForSeconds(0.6f);

            action();
        }
    }
}

