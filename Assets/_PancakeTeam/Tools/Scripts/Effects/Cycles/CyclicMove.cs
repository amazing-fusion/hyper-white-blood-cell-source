using UnityEngine;
using System.Collections.Generic;
using MovementEffects;

namespace com.PancakeTeam {
    public class CyclicMove : OptimizedBehaviour, ITickable
    {
        [SerializeField]
        Vector2 _translation;

        [SerializeField]
        float _duration;

        [SerializeField]
        float _sleep;

        [SerializeField]
        Vector2 _sleepPosition;

        [SerializeField]
        bool _inverseLoop;

        [SerializeField]
        float _delay;

        [SerializeField]
        EasingCurves.Curve _xCurve = EasingCurves.Curve.SineEaseOut;

        [SerializeField]
        EasingCurves.Curve _yCurve = EasingCurves.Curve.SineEaseOut;

        float _startTime;
        float _endTime;

        Vector2 _startPosition;

        public Vector2 Translation
        {
            get
            {
                return _translation;
            }
        }

        public float Duration
        {
            get
            {
                return _duration;
            }
        }

        void OnEnable()
        {
            if (_duration > 0)
            {
                _startPosition = transform.localPosition;

                _startTime = _delay + Time.time;
                _endTime = _startTime + _duration;

                Transform.localPosition = _startPosition + _sleepPosition;
                UpdateManager.Instance.Add(this);
            }
        }

        void OnDestroy()
        {
            if (UpdateManager.HasInstance)
            {
                UpdateManager.Instance.Remove(this);
            }
        }

        void OnDisable()
        {
            Transform.localPosition = _startPosition;
            if (UpdateManager.HasInstance)
            {
                UpdateManager.Instance.Remove(this);
            }
        }

        /*void OnTransformParentChanged()
        {
            _startPosition = transform.localPosition;

            _startTime = _delay + Time.time;
            _endTime = _startTime + _duration;

            transform.localPosition = _startPosition + _sleepPosition;
        }*/

        public void SetStartPosition(Vector2 position)
        {
            _startPosition = position;
        }

        public void Tick(float realDeltaTime)
        {
            if (!_inverseLoop)
            {
                DoTranslate();
            }
            else
            {
                if (Time.time > _endTime)
                {
                    _startTime = Time.time + _sleep;
                    _endTime = _startTime + _duration;

                    Transform.localPosition = _startPosition + _sleepPosition;

                    DoTranslate();
                }
                else
                {
                    DoTranslate();
                }
            }
        }

        void DoTranslate()
        {
            if (Time.time >= _startTime) {
                Transform.localPosition = new Vector2(
                        (float)EasingCurves.Evaluate(_xCurve, Time.time - _startTime, _startPosition.x, _translation.x, _duration),
                        (float)EasingCurves.Evaluate(_yCurve, Time.time - _startTime, _startPosition.y, _translation.y, _duration));
            }
        }
    }
}