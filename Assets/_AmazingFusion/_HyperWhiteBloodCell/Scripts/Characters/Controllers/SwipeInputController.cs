using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    [RequireComponent(typeof(IMotor))]
    public class SwipeInputController : OptimizedBehaviour, ITickable {

        [SerializeField]
        DamageController _damageController;


        [SerializeField]
        float _sqrMinSwipe;

        bool _isSwiping;

        List<Vector2> _swipePoints = new List<Vector2>();

        IMotor _motor;

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

        void OnDisable() {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody != null) {
                rigidbody.Sleep();
            }
            if (UpdateManager.HasInstance) {
                UpdateManager.Instance.Remove(this);
            }
        }

        void Initialize(Room room) {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody != null) {
                rigidbody.WakeUp();
            }
            UpdateManager.Instance.Add(this);
        }

        public void Tick(float realDeltaTime) {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0)) {
                BeginSwipe();
            } else if (Input.GetMouseButtonUp(0)) {
                EndSwipe();
            } else if (Input.GetMouseButton(0)) {
                Swipe();
            }
#else
            if (Input.touches.Length == 1) {
                if (Input.touches[0].phase == TouchPhase.Began) {

                    BeginSwipe();
                } else if (Input.touches[0].phase == TouchPhase.Ended || 
                        Input.touches[0].phase == TouchPhase.Canceled) {

                    EndSwipe();
                } else {

                    Swipe();
                }
            }
#endif
        }

        public void BeginSwipe() {
            if (!_isSwiping && _damageController.IsAlive) {
                _isSwiping = true;
                _swipePoints.Add(Input.mousePosition);
            }
        }

        public void Swipe() {
            if (_isSwiping) {
                if (_swipePoints.Count > 10) {
                    _swipePoints.RemoveAt(0);
                }

                _swipePoints.Add(Input.mousePosition);
            }
        }

        public void EndSwipe() {
            if (_isSwiping) {
                Vector3 vector = _swipePoints[_swipePoints.Count - 1] - _swipePoints[0];

                if (vector.sqrMagnitude >= _sqrMinSwipe) {
                    _motor.Translate(vector);
                }

                _swipePoints.Clear();
                _isSwiping = false;
            }
        }
    }
}