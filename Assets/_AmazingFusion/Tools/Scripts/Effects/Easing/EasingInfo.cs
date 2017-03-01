using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion {

    [System.Serializable]
    public class EasingInfo {

        [SerializeField]
        double _startValue;

        [SerializeField]
        double _changeValue;

        [SerializeField]
        EasingCurves.Curve _curve;

        double _currentValue;

        public double StartValue {
            get {
                return _startValue;
            }
            set {
                _startValue = value;
            }
        }

        public double ChangeValue {
            get {
                return _changeValue;
            }
            set {
                _changeValue = value;
            }
        }

        public EasingCurves.Curve Curve {
            get {
                return _curve;
            }
            set {
                _curve = value;
            }
        }

        public double CurrentValue {
            get {
                return _currentValue;
            }
        }

        public void Update(double currentTime, double duration) {
            _currentValue = EasingCurves.Evaluate(_curve, currentTime, _startValue, _changeValue, duration);
        }
    }
}