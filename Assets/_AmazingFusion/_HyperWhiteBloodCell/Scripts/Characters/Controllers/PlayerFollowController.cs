using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    [RequireComponent(typeof(IMotor))]
    public class PlayerFollowController : OptimizedBehaviour, ITickable {
        
        [SerializeField]
        float _updateMovementRate;

        IMotor _motor;

        float _nextUpdateMovementTime;

        void OnDestroy() {
            Room.OnLevelStart -= Initialize;
            if (UpdateManager.HasInstance) {
                UpdateManager.Instance.Remove(this);
            }
        }

        void OnDisable() {
            if (UpdateManager.HasInstance) {
                UpdateManager.Instance.Remove(this);
            }

            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody != null) {
                rigidbody.Sleep();
                rigidbody.gravityScale = 0;
                rigidbody.velocity = Vector2.zero;
            }
        }

        void Start() {
            _motor = GetComponent<IMotor>();
            if (LevelManager.Instance.CurrentRoom != null && 
                    LevelManager.Instance.CurrentRoom.Started) {
                Initialize(LevelManager.Instance.CurrentRoom);
            } else {
                Room.OnLevelStart += Initialize;
            }
        }


        void Initialize(Room room) {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody != null) {
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