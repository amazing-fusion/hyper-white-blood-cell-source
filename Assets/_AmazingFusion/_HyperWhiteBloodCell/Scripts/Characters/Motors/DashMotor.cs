using MovementEffects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    [RequireComponent(typeof(Rigidbody2D))]
    public class DashMotor : OptimizedBehaviour, IMotor {

        [SerializeField]
        float _dashSpeed;

        [SerializeField]
        float _dashDuration;

        [SerializeField]
        string[] _dashImmuneTags;

        [SerializeField]
        string _dashingTag;

        List<string> _harmfulTagsImmunity = new List<string>();

        Rigidbody2D _rigidBody;
        DamageController _damageController;

        string _tag;
        bool _isDashing;

        void Awake() {
            _rigidBody = GetComponent<Rigidbody2D>();
            _damageController = GetComponent<DamageController>();

            if (!String.IsNullOrEmpty(_dashingTag)) {
                _tag = tag;
            }
        }

        public void Translate(Vector2 vector) {
            BeginDash(vector);
        }

        void BeginDash(Vector2 vector) {
            if (!_isDashing) {
                _isDashing = true;
                _rigidBody.gravityScale = 0;
                _rigidBody.velocity = vector.normalized * _dashSpeed;

                if (_damageController != null) {
                    foreach (string immuneTag in _dashImmuneTags) {
                        if (_damageController.HarmfulTags.Contains(immuneTag)) {
                            _harmfulTagsImmunity.Add(immuneTag);
                            _damageController.HarmfulTags.Remove(immuneTag);
                        }
                    }
                }

                if (!String.IsNullOrEmpty(_dashingTag)) {
                    tag = _dashingTag;
                }

                Timing.CallDelayed(_dashDuration, EndDash);
            }
        }

        void EndDash() {
            if (_isDashing) {
                _rigidBody.velocity = Vector2.zero;
                _rigidBody.gravityScale = 1;

                if (_damageController != null) {
                    foreach (string harmfulTag in _harmfulTagsImmunity) {
                        _damageController.HarmfulTags.Add(harmfulTag);
                    }
                }

                if (!String.IsNullOrEmpty(_dashingTag)) {
                    tag = _tag;
                }
                _harmfulTagsImmunity.Clear();

                _isDashing = false;
            }
        }
    }
}