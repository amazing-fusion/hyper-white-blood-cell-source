using MovementEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class EffectsControllerPlayer : OptimizedBehaviour
    {
        [SerializeField]
        DashMotor _dashMotorPlayer;

        [SerializeField]
        DamageController _damageControllerPlayer;

        [SerializeField]
        SpriteRenderer _spriteRenderer;

        [SerializeField]
        float _magnitudeShake;

        [SerializeField]
        float _roughnessShake;

        [SerializeField]
        float _fadeInTimeShake;

        [SerializeField]
        float _fadeOutTimeShake;

        [SerializeField]
        ParticleSystem _explosionDied;

        [SerializeField]
        ParticleSystem _dashParticles0;

        [SerializeField]
        ParticleSystem _dashParticles1;

        [SerializeField]
        TrailRenderer _dashTrailRenderer;

        void Awake()
        {
            _dashMotorPlayer.OnEndDrag += DashEffectPlayerDisenabled;
            _dashMotorPlayer.OnBeginDrag += DashEffectPlayerEnabled;
            _damageControllerPlayer.OnTakeDamage += WhiteEffect;
            _damageControllerPlayer.OnDie += EffectDiedPlayer;
        }

        void Start()
        {
         //   Initialize();
        }

        void Initialize()
        {
            _explosionDied.Stop();
            _dashParticles0.Stop();
            _dashParticles1.Stop();
            _dashTrailRenderer.enabled = false;
        }
        


        public void DashEffectPlayerEnabled()
        {
           /* _dashParticles0.Play();
            _dashParticles1.Play();
            _dashTrailRenderer.enabled = true;*/
           // AnimatorControllerPlayer.Instance.AnimationAtkPlayer();

        }

        public void DashEffectPlayerDisenabled()
        {
          /*  _dashParticles0.Stop();
            _dashParticles1.Stop();
            _dashTrailRenderer.enabled = false;*/
           // AnimatorControllerPlayer.Instance.AnimationIdlePlayer();
        }

        public void EffectDiedPlayer(System.Action action)
        {
            Timing.RunCoroutine(DoEffectsDied(action)); 
        }

        IEnumerator<float> DoEffectsDied(System.Action action)
        {
            _explosionDied.Play();
            AnimatorControllerPlayer.Instance.AnimationDiedPlayer();
            EZCameraShake.CameraShaker.Instance.ShakeOnce
                (_magnitudeShake, _roughnessShake, _fadeInTimeShake, _fadeOutTimeShake);

            yield return Timing.WaitForSeconds(1f);

            action();
        }

        public void WhiteEffect()
        {
            Timing.RunCoroutine(DoWhiteSprite());
        }
        
        IEnumerator<float> DoWhiteSprite()
        {
            White.Instance.WhiteSprite(_spriteRenderer);
            yield return Timing.WaitForSeconds(0.2f);
            White.Instance.NormalSprite(_spriteRenderer);
        }
    }

}

