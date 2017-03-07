using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    [RequireComponent(typeof(Collider2D))]
    public class DamageController : OptimizedBehaviour {

        [SerializeField]
        int _lifes;

        [SerializeField]
        Collider2D _collider;

        [SerializeField]
        List<string> _harmfulTags;

        //[SerializeField]
        //float _immortalityTimeAfterDamage;

        [SerializeField]
        SpriteColorFlick _immortalityEffect;

        int _currentLifes;

        bool _isImmortal;

        bool _isAlive;

		public event System.Action<int> OnLifesChange;
        public event System.Action OnDieEnd;

        public event System.Action OnTakeDamage;

        public event System.Action<System.Action> OnDie;

        public int CurrentLifes {
            get {
                return _currentLifes;
            }

            set {
                if (value != _currentLifes) {
                    _currentLifes = value;
                    if (OnLifesChange != null) OnLifesChange(_currentLifes);
                    if (_currentLifes <= 0) {
                        Die();
                    }
                }
            }
        }

        public List<string> HarmfulTags {
            get {
                return _harmfulTags;
            }
        }

        public bool IsAlive
        {
            get
            {
                return _isAlive;
            }

            set
            {
                _isAlive = value;
            }
        }

        public void Initialize() {
            CurrentLifes = _lifes;
            _collider.enabled = true;
            IsAlive = true;
        }

        void Awake() {
            Initialize();
        }

        public void Die() {
            IsAlive = false;
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody != null) {
                rigidbody.velocity = Vector3.zero;
                rigidbody.Sleep();
            }
            _collider.enabled = false;
            if (OnDie != null) OnDie(DieAnimationEnded);
        }

        public void SetImmortal(float duration) {
            _isImmortal = true;
            //TODO: Start Effect
            _immortalityEffect.enabled = true;
            MovementEffects.Timing.CallDelayed(duration, () => {
                //TODO: End Effect
                _immortalityEffect.enabled = false;
                _isImmortal = false;
            });
        }

        void DieAnimationEnded() {
            Debug.Log("DieAnimationEnded");
            if (OnDieEnd != null) OnDieEnd();
            this.gameObject.SetActive(false);
        }

        void TakeDamage() {
            Debug.Log("TakeDamage");
            if (!_isImmortal) {
                --CurrentLifes;
                if (OnTakeDamage != null) OnTakeDamage();

                //if (_immortalityTimeAfterDamage > 0) {
                //    SetImmortal(_immortalityTimeAfterDamage);
                //}
            }
        }

        void OnTriggerEnter2D(Collider2D collider) {
            Debug.Log("OnTriggerEnter2D");
            if (_harmfulTags.Contains(collider.tag)) {
                TakeDamage();
            }
        }

        void OnCollisionEnter2D(Collision2D collision) {
            Debug.Log("OnCollisionEnter2D");
            if (_harmfulTags.Contains(collision.collider.tag)) {
                TakeDamage();
            }
        }
    }
}