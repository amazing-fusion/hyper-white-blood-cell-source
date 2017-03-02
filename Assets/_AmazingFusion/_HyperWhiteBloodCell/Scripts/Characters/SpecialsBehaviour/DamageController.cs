using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    public class DamageController : OptimizedBehaviour {

        [SerializeField]
        float _lifes;

        [SerializeField]
        List<string> _harmfulTags;

        bool _died;
        float _currentLifes;

        public event System.Action OnLifesChange;
        public event System.Action OnDieEnd;

        public event System.Action OnTakeDamage;

        public event System.Action<System.Action> OnDie;

        public float CurrentLifes {
            get {
                return _currentLifes;
            }

            set {
                if (value != _currentLifes) {
                    _currentLifes = value;
                    Debug.Log("Lifes: " + _currentLifes);
                    if (OnLifesChange != null) OnLifesChange();
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

        public void Initialize() {
            CurrentLifes = _lifes;
        }

        void Awake() {
            Initialize();
        }

        void Die() {
            if (OnDie != null) OnDie(DieAnimationEnded);
        }

        void DieAnimationEnded() {
            Debug.Log("DieAnimationEnded");
            if (OnDieEnd != null) OnDieEnd();
        }

        void TakeDamage() {
            Debug.Log("TakeDamage");
            --CurrentLifes;
            if (OnTakeDamage != null) OnTakeDamage();
        }

        void OnTriggerEnter2D(Collider2D collider) {
            Debug.Log("OnTriggerEnter2D");
            if (_harmfulTags.Contains(collider.tag)) {
                TakeDamage();
            }
        }

        void OnCollisionEnter2D(Collision2D collision) {
            if (_harmfulTags.Contains(collision.collider.tag)) {
                TakeDamage();
            }
        }
    }
}