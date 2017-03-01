using UnityEngine;
using System.Collections;

namespace com.AmazingFusion {
    public class CyclicScale : OptimizedBehaviour, ITickable
    {
        [SerializeField]
        Vector2 _rescale;

        [SerializeField]
        float _duration;

        [SerializeField]
        bool _inverseLoop;

        [SerializeField]
        float _delay;

        [SerializeField]
        float _sleep;

        [SerializeField]
        Vector2 _sleepScale;

        [SerializeField]
        EasingCurves.Curve _xCurve = EasingCurves.Curve.SineEaseOut;

        [SerializeField]
        EasingCurves.Curve _yCurve = EasingCurves.Curve.SineEaseOut;

        float _startTime;
        float _endTime;
        Vector3 _startScale;

        Vector2 _baseScale;
        Vector2 _modScale;

        public Vector2 Rescale
        {
            get
            {
                return _rescale;
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
                _startScale = Transform.localScale;

                _baseScale = _inverseLoop ? _startScale : _startScale + (Vector3)_rescale / 2;
                _modScale = _inverseLoop ? _rescale : _rescale / 2;

                _startTime = Time.time + _delay;
                _endTime = _startTime + _duration;

                Transform.localScale = _startScale + (Vector3)_sleepScale;
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
            Transform.localScale = _startScale;
            if (UpdateManager.HasInstance)
            {
                UpdateManager.Instance.Remove(this);
            }
        }

        public void Initialize(Vector3 rescale, float duration, bool inverseLoop = false)
        {
            _rescale = rescale;
            _duration = duration;
            _inverseLoop = inverseLoop;

            _startScale = Transform.localScale;

            _baseScale = _inverseLoop ? _startScale : _startScale + (Vector3)_rescale / 2;
            _modScale = _inverseLoop ? _rescale : _rescale / 2;

            _startTime = Time.time + _delay;
            _endTime = _startTime + _duration;
            UpdateManager.Instance.Add(this);
        }

        public void SetStartScale(float scale)
        {
            _startScale = Transform.localScale;
        }

        public void Tick(float realDeltaTime)
        {
            if (!_inverseLoop)
            {
                DoRescale();
            }
            else
            {
                if (Time.time > _endTime)
                {
                    _startTime = Time.time + _sleep;
                    _endTime = _startTime + _duration;
                    Transform.localScale = _startScale + (Vector3)_sleepScale;

                    DoRescale();
                }
                else
                {
                    DoRescale();
                }
            }
        }

        void DoRescale()
        {
            if (Time.time >= _startTime) {
                float duration = _inverseLoop ? _duration * 2 : _duration;
                Transform.localScale = new Vector3(
                            (float)EasingCurves.Evaluate(_xCurve, Time.time - _startTime, _baseScale.x, _modScale.x, duration),
                            (float)EasingCurves.Evaluate(_yCurve, Time.time - _startTime, _baseScale.y, _modScale.y, duration),
                            0);

            }
        }
    }
}
