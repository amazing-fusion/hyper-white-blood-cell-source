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