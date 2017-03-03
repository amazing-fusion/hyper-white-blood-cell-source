using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    [RequireComponent(typeof(IMotor))]
    public class PathController : OptimizedBehaviour, ITickable {

        [SerializeField]
        Transform[] _wayPointsPath;

        [SerializeField]
        bool _invertPathOnEnd;

        [SerializeField]
        float _reachDistance;

        [SerializeField]
        float _updateMovementRate = -1f;

        float _nextUpdateMovementTime;

        bool _inverse;
        int _currentPathIndex;

        IMotor _motor;

        void Awake() {
            Initialize();
        }

        public void Initialize(Transform[] wayPoints, bool invertPathOnEnd = false) {
            _wayPointsPath = wayPoints;
            _invertPathOnEnd = invertPathOnEnd;
            Initialize();
        }

        public void Initialize() {
            if (_wayPointsPath.Length < 2) return;

            _motor = GetComponent<IMotor>();

            _currentPathIndex = 0;
            UpdateManager.Instance.Add(this);

            _motor.Translate(_wayPointsPath[_currentPathIndex].position);
        }

        void OnDestroy() {
            if (UpdateManager.HasInstance) {
                UpdateManager.Instance.Remove(this);
            }
        }

        public void Tick(float realDeltaTime) {
            float distance = Vector2.Distance(_wayPointsPath[_currentPathIndex].position, Transform.position);
            
            if (distance <= _reachDistance) {
                NextWayPoint();  
            } else if (_nextUpdateMovementTime >= 0 && Time.time >= _nextUpdateMovementTime) {
                _motor.Translate(_wayPointsPath[_currentPathIndex].position - Transform.position);
                _nextUpdateMovementTime = Time.time + _updateMovementRate;
            }
        }

        void NextWayPoint() {
            Debug.Log("NextWayPoint");
            if (_inverse) {
                if (_currentPathIndex > 0) {
                    --_currentPathIndex;
                } else {
                    _inverse = false;
                    ++_currentPathIndex;
                }
            } else {
                if (_currentPathIndex < _wayPointsPath.Length - 1) {
                    ++_currentPathIndex;
                } else {
                    if (_invertPathOnEnd) {
                        _inverse = true;
                        --_currentPathIndex;
                    }
                    else {
                        _currentPathIndex = 0;
                    }
                }
            }

            _motor.Translate(_wayPointsPath[_currentPathIndex].position - Transform.position);
        }
    }
}