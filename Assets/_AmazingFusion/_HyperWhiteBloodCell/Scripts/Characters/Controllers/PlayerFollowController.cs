using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    [RequireComponent(typeof(IMotor))]
    public class PlayerFollowController : OptimizedBehaviour, ITickable {
        
        [SerializeField]
        float _updateMovementRate;

        DamageController _damageController;
        IMotor _motor;

        float _nextUpdateMovementTime;

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

        void LevelEnd(Room room) {
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

            _nextUpdateMovementTime = Time.time + _updateMovementRate;
            UpdateManager.Instance.Add(this);
        }

        public void Tick(float realDeltaTime) {
            if (Time.time >= _nextUpdateMovementTime) {
                _motor.Translate(GameController.Instance.Player.Transform.position - Transform.position);
                _nextUpdateMovementTime = Time.time + _updateMovementRate;
            }
        }
    }
}