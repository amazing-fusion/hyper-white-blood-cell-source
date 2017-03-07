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

        //[SerializeField]
        //float _sqrSwipe;

        //bool _swipe;
        bool _isSwiping;

        Vector2 _startPoint;
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

        void Initialize(Room room) {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody != null) {
                rigidbody.WakeUp();
                MovementEffects.Timing.CallDelayed(0.01f, () => { rigidbody.gravityScale = 1; });
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
                if (!_isSwiping) {
                    BeginSwipe();
                } else {
                    Swipe();

                    // Swipe when stop
                    //if (_swipe) {
                    //    Vector2 vector = (Vector2)Input.mousePosition - _swipePoints[0];
                    //    if (vector.sqrMagnitude <= _sqrSwipe) {
                    //        EndSwipe(vector);
                    //    }
                    //} else {
                    //    Vector2 vector = _startPoint - _swipePoints[0];
                    //    if (vector.sqrMagnitude >= _sqrMinSwipe) {
                    //        _swipe = true;
                    //    }
                    //}

                    // Swipe every frame
                    //if (_swipePoints.Count < 2) {
                    //    Swipe();
                    //} else {
                    //    Vector3 vector = _swipePoints[_swipePoints.Count - 1] - _swipePoints[0];
                    //    Debug.Log(vector.sqrMagnitude);
                    //    if (vector.sqrMagnitude >= _sqrSwipe) {
                    //        EndSwipe();
                    //    } else {
                    //        Swipe();
                    //    }
                    //}
                }
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
                _startPoint = Input.mousePosition;
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
                Vector2 vector = _swipePoints[_swipePoints.Count - 1] - _swipePoints[0];

                EndSwipe(vector);
            }
        }

        public void EndSwipe(Vector2 vector) {
            if (_isSwiping) {
                //Vector2 vector = _swipePoints[_swipePoints.Count - 1] - _swipePoints[0];

                if (vector.sqrMagnitude >= _sqrMinSwipe) {
                    _motor.Translate(vector);
                }

                _swipePoints.Clear();
                //_swipe = false;
                _isSwiping = false;
            }
        }
    }
}