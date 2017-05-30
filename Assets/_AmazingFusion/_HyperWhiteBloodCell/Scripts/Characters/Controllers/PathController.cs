using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCellDash {
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

        DamageController _damageController;

        float _nextUpdateMovementTime;

        bool _inverse;
        int _currentPathIndex;

        IMotor _motor;

        void Start() {
            _motor = GetComponent<IMotor>();
            _damageController = GetComponent<DamageController>();
            if (_damageController != null) {
                _damageController.OnDie += Death;
            }

            if (LevelManager.Instance.CurrentRoom != null && 
                    LevelManager.Instance.CurrentRoom.Started) {
                Initialize(LevelManager.Instance.CurrentRoom);
            } else {
                Room.OnLevelStart += Initialize;
            }
            Room.OnLevelEnd += LevelEnd;
        }


        void Initialize(Room room) {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody != null && rigidbody.bodyType == RigidbodyType2D.Dynamic) {
                rigidbody.WakeUp();
                MovementEffects.Timing.CallDelayed(0.01f, () => { rigidbody.gravityScale = 1; });
            }

            if (_wayPointsPath.Length < 2) return;

            _currentPathIndex = 0;
            _motor.Translate(_wayPointsPath[_currentPathIndex].position);
            UpdateManager.Instance.Add(this);
        }

        public void SetPath(Transform[] wayPoints, bool invertPathOnEnd = false) {
            _wayPointsPath = wayPoints;
            _invertPathOnEnd = invertPathOnEnd;
        }


        void OnDestroy() {
            Room.OnLevelStart -= Initialize;
            Room.OnLevelEnd -= LevelEnd;
            if (UpdateManager.HasInstance) {
                UpdateManager.Instance.Remove(this);
            }
            if (_damageController != null) {
                _damageController.OnDie -= Death;
            }
        }

        void OnDisable() {
            End();
        }

        void Death(Action onDieEnd) {
            End();
        }

        void LevelEnd(Room room, bool win) {
            End();
        }

        void End() {
            if (UpdateManager.HasInstance) {
                UpdateManager.Instance.Remove(this);
            }

            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody && rigidbody.bodyType == RigidbodyType2D.Dynamic) {
                rigidbody.Sleep();
                rigidbody.gravityScale = 0;
                rigidbody.velocity = Vector2.zero;

            }
        }

        public void Tick(float realDeltaTime) {
            float distance = Vector2.Distance(_wayPointsPath[_currentPathIndex].position, Transform.position);
            
            if (distance <= _reachDistance) {

                NextWayPoint();

            } else if (_updateMovementRate >= 0 && Time.time >= _nextUpdateMovementTime) {
                _motor.Translate(_wayPointsPath[_currentPathIndex].position - Transform.position);
                _nextUpdateMovementTime = Time.time + _updateMovementRate;
            }
        }

        void NextWayPoint() {
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