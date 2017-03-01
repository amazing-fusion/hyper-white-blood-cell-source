﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    public class EasingMotor : OptimizedBehaviour, IMotor, ITickable {

        [SerializeField]
        EasingCurves.Curve _curve;

        [SerializeField]
        float _speed;

        float _translateDuration;
        Vector2 _destinyPosition;

        bool _isMoving;

        Vector2 _moveVector;
        float _startMoveTime;
        Vector2 _startMovePosition;

        void OnDestroy() {
            if (_isMoving && UpdateManager.HasInstance) {
                UpdateManager.Instance.Add(this);
            }
        }

        public void Translate(Vector2 vector) {
            _moveVector = vector;
            _translateDuration = vector.magnitude / _speed;

            _startMoveTime = Time.time;
            _startMovePosition = Transform.position;

            if (!_isMoving) {
                UpdateManager.Instance.Add(this);
            }
        }

        public void Tick(float realDeltaTime) {
            float time = Time.time - _startMoveTime;
            if (time > _translateDuration) {
                Transform.position = _startMovePosition + _moveVector;
                UpdateManager.Instance.Remove(this);
                return;
            }

            Transform.position = _startMovePosition + _moveVector * (float)EasingCurves.Evaluate(
                    _curve,
                    time,
                    0,
                    1,
                    _translateDuration);
        }
    }
}