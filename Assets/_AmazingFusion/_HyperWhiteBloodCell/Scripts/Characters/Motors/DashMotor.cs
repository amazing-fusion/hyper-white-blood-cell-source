using MovementEffects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    public class DashMotor : OptimizedBehaviour, IMotor {

        [SerializeField]
        float _dashSpeed;

        [SerializeField]
        float _dashDuration;

        Rigidbody2D _rigidBody;

        bool _isDashing;

        void Awake() {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        public void MoveTo(Vector2 destinyPosition) {
            _isDashing = true;
            _rigidBody.gravityScale = 0;
            _rigidBody.velocity = destinyPosition.normalized * _dashSpeed;

            Timing.CallDelayed(_dashDuration, EndDash);
        }

        void EndDash() {
            if (_isDashing) {
                _rigidBody.velocity = Vector2.zero;
                _rigidBody.gravityScale = 1;
            }
        }
    }
}