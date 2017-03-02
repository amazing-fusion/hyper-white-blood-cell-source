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

        void Awake()
        {
            _dashMotorPlayer.OnEndDrag += DashEffectPlayerDisenabled;
            _dashMotorPlayer.OnBeginDrag += DashEffectPlayerEnabled;
            _damageControllerPlayer.OnTakeDamage += WhiteEffect;
            _explosionDied.Stop();
        }
        


        public void DashEffectPlayerEnabled()
        {
            /*
             Trail enabled
             Particles enabled
             */
        }

        public void DashEffectPlayerDisenabled()
        {
             /*Trail disenabled
             Particles disenabled
             */
        }

        public void EffectDiedPlayer()
        {
            _explosionDied.Play();
            AnimatorControllerPlayer.Instance.AnimationDiedPlayer();
            EZCameraShake.CameraShaker.Instance.ShakeOnce(_magnitudeShake,_roughnessShake,_fadeInTimeShake,_fadeOutTimeShake);
        }

        public void WhiteEffect()
        {
            Timing.RunCoroutine(DoWhiteSprite());
        }
        
        IEnumerator<float> DoWhiteSprite()
        {
            White.Instance.WhiteSprite(_spriteRenderer);
            yield return Timing.WaitForSeconds(0.1f);
            White.Instance.NormalSprite(_spriteRenderer);
        }
    }

}

