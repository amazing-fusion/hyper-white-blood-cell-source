using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCellDash {
    [RequireComponent(typeof(IMotor))]
    public class SwipeInputController : OptimizedBehaviour, ITickable {

        [SerializeField]
        float _sqrMinSwipe;

        //[SerializeField]
        //float _sqrSwipe;

        //bool _swipe;
        bool _isSwiping;

        DamageController _damageController;

        //Vector2 _startPoint;
        List<Vector2> _swipePoints = new List<Vector2>();

        IMotor _motor;

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

        void Initialize(Room room) {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody != null && rigidbody.bodyType == RigidbodyType2D.Dynamic) {
                rigidbody.WakeUp();
                MovementEffects.Timing.CallDelayed(0.01f, () => { rigidbody.gravityScale = 1; });
            }

            UpdateManager.Instance.Add(this);
        }

        public void Tick(float realDeltaTime) {
#if UNITY_EDITOR
            if (!_isSwiping && Input.GetMouseButtonDown(0)) {
                BeginSwipe();
            } else if (_isSwiping && Input.GetMouseButtonUp(0)) {
                Swipe();
                EndSwipe();
            } else if (_isSwiping && Input.GetMouseButton(0)) {
                Swipe();
                Vector2 vector = _swipePoints[_swipePoints.Count - 1] - _swipePoints[0];
                if (vector.sqrMagnitude >= _sqrMinSwipe) {
                    EndSwipe(vector);
                }

            }
#else
            if (Input.touches.Length == 1) {
                if (Input.touches[0].phase == TouchPhase.Began) {

                    BeginSwipe();
                } else if (_isSwiping && (Input.touches[0].phase == TouchPhase.Ended || 
                        Input.touches[0].phase == TouchPhase.Canceled)) {

                    Swipe();
                    EndSwipe();
                } else if (_isSwiping) {
                    Swipe();
                    Vector2 vector = _swipePoints[_swipePoints.Count - 1] - _swipePoints[0];
                    if (vector.sqrMagnitude >= _sqrMinSwipe) {
                        EndSwipe(vector);
                    }
                }
            }
#endif
        }

        public void BeginSwipe() {
            //if (!_isSwiping) {
                _isSwiping = true;
                //_startPoint = Input.mousePosition;
                _swipePoints.Add(Input.mousePosition);
            //}
        }

        public void Swipe() {
            //if (_isSwiping) {
                if (_swipePoints.Count > 20) {
                    _swipePoints.RemoveAt(0);
                }

                _swipePoints.Add(Input.mousePosition);
            //}
        }

        public void EndSwipe() {
            //if (_isSwiping) {
                Vector2 vector = _swipePoints[_swipePoints.Count - 1] - _swipePoints[0];

                EndSwipe(vector);
            //}
        }

        public void EndSwipe(Vector2 vector) {
            //if (_isSwiping) {
                //Vector2 vector = _swipePoints[_swipePoints.Count - 1] - _swipePoints[0];

                if (vector.sqrMagnitude >= _sqrMinSwipe) {
                    _motor.Translate(vector);
                }

                _swipePoints.Clear();
                //_swipe = false;
                _isSwiping = false;
            //}
        }
    }
}