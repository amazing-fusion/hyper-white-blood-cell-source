using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace com.PancakeTeam {
    [RequireComponent(typeof(TMP_Text))]
    public class TextProColorFlick : OptimizedBehaviour, ITickable
    {
        [SerializeField]
        Color _startColor;

        [SerializeField]
        Color _endColor;

        [SerializeField]
        float _duration;

        TMP_Text _text;

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

        void Start()
        {
            UpdateManager.Instance.Add(this);

            _text = GetComponent<TMP_Text>();
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
            float mod = (float)EasingCurves.SineEaseOut(Time.time, 0.5, 0.5, _duration);

            float r = _startColor.r + (_endColor.r - _startColor.r) * mod;
            float g = _startColor.g + (_endColor.g - _startColor.g) * mod;
            float b = _startColor.b + (_endColor.b - _startColor.b) * mod;
            float a = _startColor.a + (_endColor.a - _startColor.a) * mod;

            _text.color = new Color(r, g, b, a);
        }
    }
}
