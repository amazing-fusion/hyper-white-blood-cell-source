using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace com.PancakeTeam {
    [RequireComponent(typeof(Image))]
    public class ImageColorFlick : OptimizedBehaviour, ITickable
    {
        [SerializeField]
        Color _startColor;

        [SerializeField]
        Color _endColor;

        [SerializeField]
        float _duration;

        [SerializeField]
        float _delay;

        [SerializeField]
        float _sleep;

        [SerializeField]
        Color _sleepColor;

        [SerializeField]
        bool _inverseLoop;

        [SerializeField]
        EasingCurves.Curve _curve = EasingCurves.Curve.SineEaseOut;

        Image _image;

        float _baseMod;
        float _finalMod;

        float _startTime;
        float _endTime;

        public Color StartColor
        {
            get
            {
                return _startColor;
            }

            set
            {
                _startColor = value;
            }
        }

        public Color EndColor
        {
            get
            {
                return _endColor;
            }

            set
            {
                _endColor = value;
            }
        }

        public float Duration
        {
            get
            {
                return _duration;
            }

            set
            {
                _duration = value;
            }
        }

        void Awake() {
            _image = GetComponent<Image>();

            _baseMod = _inverseLoop ? 0 : 0.5f;
            _finalMod = _inverseLoop ? 1 : 0.5f;
        }

        void OnEnable()
        {
            if (_duration > 0) {
                _startTime = _delay + Time.time;
                _endTime = _startTime + _duration;

                _image.color = _sleepColor;
                UpdateManager.Instance.Add(this);
            }
        }

        void OnDisable() {
            if (UpdateManager.HasInstance) {
                UpdateManager.Instance.Remove(this);
            }
        }

        void OnDestroy()
        {
            if (UpdateManager.HasInstance)
            {
                UpdateManager.Instance.Remove(this);
            }
        }

        public void Tick(float realDeltaTime)
        {
            if (!_inverseLoop) {
                DoFlick();
            } else {
                if (Time.time > _endTime) {
                    _startTime = _delay + Time.time;
                    _endTime = _startTime + _duration;

                    _image.color = _sleepColor;

                    DoFlick();
                } else {
                    DoFlick();
                }
            }
        }

        void DoFlick() {
            if (Time.time >= _startTime) {
                float duration = _inverseLoop ? _duration * 2 : _duration;
                float mod = (float)EasingCurves.Evaluate(_curve, Time.time - _startTime, _baseMod, _finalMod, duration);

                float r = _startColor.r + (_endColor.r - _startColor.r) * mod;
                float g = _startColor.g + (_endColor.g - _startColor.g) * mod;
                float b = _startColor.b + (_endColor.b - _startColor.b) * mod;
                float a = _startColor.a + (_endColor.a - _startColor.a) * mod;

                _image.color = new Color(r, g, b, a);
            }
        }
    }
}
