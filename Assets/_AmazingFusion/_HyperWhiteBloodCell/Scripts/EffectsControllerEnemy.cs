using MovementEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class EffectsControllerEnemy : OptimizedBehaviour
    {
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

        [SerializeField]
        Color _colorDamage;

        DamageController _damageController;
        SpriteRenderer _spriteEnemy;
        AnimatorControllerEnemy _animator;
        ParticleSystem _explosionParticles;
        Animator _animatorController;
        SpriteRenderer _spriteExplosionController;

        void Awake()
        {
            
        }

        void OnDisable()
        {
            Initialize();
        }

        void Start()
        {
            Initialize();
        }

        public  void Initialize()
        {
            _damageController = GetComponent<DamageController>();
            _animator = GetComponent<AnimatorControllerEnemy>();
            _explosionParticles = Transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
            _spriteEnemy = Transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>();
            _animatorController = Transform.GetChild(0).GetChild(0).GetComponent<Animator>();
            _spriteExplosionController = Transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();

            _animatorController.enabled = false;
            _spriteExplosionController.enabled = false;
            _damageController.OnDie += EffectsDiedEnemy;
            _damageController.OnTakeDamage += EffectScaleDamage;

            _scaleEffect.localScale = Vector3.zero;
            _explosionParticles.Stop();
        }

        void OnDestroy() {
            _damageController.OnDie -= EffectsDiedEnemy;
            _damageController.OnTakeDamage -= EffectScaleDamage;
        }

        public void EffectScaleDamage()
        {
            Timing.RunCoroutine(DoWhiteSprite());
            _scaleEffect.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 180));
            _scaleEffect.GetComponent<SequenceEasingAnimation>().Play();
            AudioController.Instance.PlayDamageEnemySound();
        }

        IEnumerator<float> DoWhiteSprite()
        {
            White.Instance.WhiteSprite(_spriteEnemy, _colorDamage);
            yield return Timing.WaitForSeconds(0.1f);
            White.Instance.NormalSprite(_spriteEnemy);
        }

        public void EffectsDiedEnemy(System.Action action)
        {
            AudioController.Instance.PlayDeathEnemySound();
            AnimationDiedEnemy();
            Timing.RunCoroutine(DoEffectsDiedEnemy(action));
        }

        public void AnimationDiedEnemy()
        {
            Debug.Log("Animacion Muerte de "
                 + _animatorController.transform.parent.parent.gameObject);
            _animatorController.enabled = true;
            _spriteExplosionController.enabled = true;
            _animatorController.Play(0);
        }

        IEnumerator<float> DoEffectsDiedEnemy(System.Action action)
        {
            _explosionParticles.Play();
            _spriteEnemy.enabled = false;
            EZCameraShake.CameraShaker.Instance.ShakeOnce
                (_magnitudeShake, _roughnessShake, _fadeInTimeShake, _fadeOutTimeShake);

            yield return Timing.WaitForSeconds(0.6f);

            action();
        }
    }
}

