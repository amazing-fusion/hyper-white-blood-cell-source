using MovementEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class EffectsControllerPlayer : OptimizedBehaviour
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
        Color _colorDamage;

        DashMotor _dashMotorPlayer;
        DamageController _damageControllerPlayer;
        SpriteRenderer _spriteRenderer;
        ParticleSystem _explosionDied;
        SequenceEasingAnimation _dashAnimation;
        
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

        void Initialize()
        {
            Debug.Log("Coloco todo");
            _dashMotorPlayer = GetComponent<DashMotor>();
            _damageControllerPlayer = GetComponent<DamageController>();
            _dashAnimation = GetComponent<SequenceEasingAnimation>();

            _spriteRenderer = Transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
            _explosionDied = Transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ParticleSystem>();

            _dashMotorPlayer.OnBeginDrag += DashEffectPlayerEnabled;
            _damageControllerPlayer.OnTakeDamage += WhiteEffect;
            _damageControllerPlayer.OnDie += EffectDiedPlayer;

            _spriteRenderer.enabled = true;
            _explosionDied.Stop();
        }
        
        public void DashEffectPlayerEnabled()
        {
            _dashAnimation.Play();
            AudioController.Instance.PlayDashPlayerSound();
        }

        public void EffectDiedPlayer(System.Action action)
        {
            AudioController.Instance.PlayDeathExplosionPlayerSound();
            Timing.RunCoroutine(DoEffectsDied(action)); 
        }

        IEnumerator<float> DoEffectsDied(System.Action action)
        {
            _explosionDied.Play();
            AnimatorControllerPlayer.Instance.AnimationDiedPlayer();
            EZCameraShake.CameraShaker.Instance.ShakeOnce
                (_magnitudeShake, _roughnessShake, _fadeInTimeShake, _fadeOutTimeShake);

            yield return Timing.WaitForSeconds(0.6f);
            
            action();
        }

        public void WhiteEffect()
        {
            Timing.RunCoroutine(DoWhiteSprite());
            AudioController.Instance.PlayDamagePlayerSound();
        }
        
        IEnumerator<float> DoWhiteSprite()
        {
            White.Instance.WhiteSprite(_spriteRenderer, _colorDamage);
            EZCameraShake.CameraShaker.Instance.ShakeOnce
                (_magnitudeShake*0.6f, _roughnessShake, _fadeInTimeShake, _fadeOutTimeShake);
            yield return Timing.WaitForSeconds(0.2f);
            White.Instance.NormalSprite(_spriteRenderer);
        }
    }

}

