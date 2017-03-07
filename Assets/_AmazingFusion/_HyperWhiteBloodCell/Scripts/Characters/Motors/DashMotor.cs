using MovementEffects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    [RequireComponent(typeof(Rigidbody2D))]
    public class DashMotor : OptimizedBehaviour, IMotor {

        [SerializeField]
        Transform _player;

        [SerializeField]
        float _dashSpeed;

        [SerializeField]
        float _dashDuration;

        [SerializeField]
        string[] _dashImmuneTags;

        [SerializeField]
        string _dashingTag;

        [SerializeField]
        float _immunityAfterDashDuration;

        List<string> _harmfulTagsImmunity = new List<string>();

        Rigidbody2D _rigidBody;
        DamageController _damageController;
        
        public event System.Action OnBeginDrag;
        public event System.Action OnEndDrag;
        
        string _tag;
        CoroutineHandle _dashCoroutine;

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
            if (_dashCoroutine != null) {
                EndDash();
            }
            if (_rigidBody != null) {
                _rigidBody.gravityScale = 0;
                _rigidBody.velocity = vector.normalized * _dashSpeed;
            }

            if (vector.x > 0) {
                _player.rotation = Quaternion.LookRotation(Vector3.forward, vector);
            } else {
                _player.rotation = Quaternion.LookRotation(Vector3.back, vector);
            }
            _player.Rotate(0, 0, 90);

            if (OnBeginDrag != null) OnBeginDrag();

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

            _dashCoroutine = Timing.CallDelayed(_dashDuration, EndDash);
        }

        void EndDash() {
            if (_dashCoroutine != null) {
                Timing.KillCoroutines(_dashCoroutine);
                _dashCoroutine = null;

                if (_rigidBody != null) {
                    _rigidBody.velocity = Vector2.zero;
                    _rigidBody.gravityScale = 1;
                }

                if (OnEndDrag != null) OnEndDrag();

                if (!String.IsNullOrEmpty(_dashingTag)) {
                    tag = _tag;
                }

                if (_damageController != null) {
                    if (_immunityAfterDashDuration > 0) {
                        Timing.CallDelayed(_immunityAfterDashDuration, () => {
                            Debug.Log("");
                            foreach (string immuneTag in _harmfulTagsImmunity) {
                                _damageController.HarmfulTags.Add(immuneTag);
                            }
                            _harmfulTagsImmunity.Clear();
                        });
                    } else {
                        foreach (string immuneTag in _harmfulTagsImmunity) {
                            _damageController.HarmfulTags.Add(immuneTag);
                        }
                        _harmfulTagsImmunity.Clear();
                    }
                }
                
            }
        }
    }
}