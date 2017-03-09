using MovementEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class EffectsControllerPlayer : Singleton<EffectsControllerPlayer>
    {
        [SerializeField]
        float _dieMagnitudeShake;

        [SerializeField]
        float _dieRoughnessShake;

        [SerializeField]
        float _dieFadeOutTimeShake;

        [SerializeField]
        float _collideBaseMagnitudeShake;

        [SerializeField]
        float _collideBaseRoughnessShake;

        [SerializeField]
        float _collideFadeOutTimeShake;

        [SerializeField]
        float _wallCollideForce;

        [SerializeField]
        Color _colorDamage;

        [SerializeField]
        EasingAnimation _cameraEffect;

        DashMotor _dashMotorPlayer;
        DamageController _damageControllerPlayer;
        SpriteRenderer _spriteRenderer;
        ParticleSystem _explosionDied;
        SequenceEasingAnimation _dashAnimation;

         void Start()
        {
            InitializeComponents();
        }

        void InitializeComponents()
        {
            _dashMotorPlayer = GetComponent<DashMotor>();
            _damageControllerPlayer = GetComponent<DamageController>();
            _dashAnimation = Transform.GetChild(0).GetComponent<SequenceEasingAnimation>();

            _spriteRenderer = Transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
            _explosionDied = Transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ParticleSystem>();

            _dashMotorPlayer.OnBeginDrag += DashEffectPlayerEnabled;
            _damageControllerPlayer.OnCollide += CollideEffect;
            _damageControllerPlayer.OnTakeDamage += WhiteEffect;
            _damageControllerPlayer.OnDie += EffectDiedPlayer;
        }

        public void Initialize()
        {
            Debug.Log("Coloco Player Effects");
            AnimatorControllerPlayer.Instance.AnimationIdlePlayer();

            _spriteRenderer.enabled = true;
            _explosionDied.Stop();
        }
        
        void OnDestroy() {
            _dashMotorPlayer.OnBeginDrag -= DashEffectPlayerEnabled;
            _damageControllerPlayer.OnCollide -= CollideEffect;
            _damageControllerPlayer.OnTakeDamage -= WhiteEffect;
            _damageControllerPlayer.OnDie -= EffectDiedPlayer;
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
                (_dieMagnitudeShake, _dieRoughnessShake, 0, _dieFadeOutTimeShake);

            yield return Timing.WaitForSeconds(0.6f);
            
            action();
        }

        public void WhiteEffect()
        {
            Timing.RunCoroutine(DoWhiteSprite());
            AudioController.Instance.PlayDamagePlayerSound();
        }
        

        public void CollideEffect(Rigidbody2D playerBody, Rigidbody2D colliderBody) {
            float force = 0;
            if (playerBody != null) {
                if (colliderBody != null) {
                    force = (playerBody.velocity - colliderBody.velocity).magnitude;
                } else {
                    force = playerBody.velocity.magnitude;
                }
            } else if (colliderBody != null) {
                force = playerBody.velocity.magnitude;
            }

            //TODO: Fix the hack: gravity velocity is not detected (is (0, 0))!!! So I use a wall force
            if (force == 0) {
                force = _wallCollideForce;
            }

            Debug.Log("Collision force: " + force);

            //if (force > 0) {
            EZCameraShake.CameraShaker.Instance.ShakeOnce
                    (_collideBaseMagnitudeShake * force,
                    _collideBaseRoughnessShake * force, 
                    0, _collideFadeOutTimeShake * force);
            //}
        }

        IEnumerator<float> DoWhiteSprite()
        {
            White.Instance.WhiteSprite(_spriteRenderer, _colorDamage);
            _cameraEffect.Play();
            yield return Timing.WaitForSeconds(0.2f);
            White.Instance.NormalSprite(_spriteRenderer);
        }
    }

}

