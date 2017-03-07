using System;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

namespace com.AmazingFusion {
    public class ColorEasingAnimation : EasingAnimation {

        [SerializeField]
        protected EasingInfo _rEasingInfo;

        [SerializeField]
        protected EasingInfo _gEasingInfo;

        [SerializeField]
        protected EasingInfo _bEasingInfo;

        [SerializeField]
        protected EasingInfo _aEasingInfo;

        public override event Action<IEffectable> OnStart;
        public override event Action<IEffectable> OnUpdate;
        public override event Action<IEffectable> OnEnd;

        protected override IEnumerator<float> DoEasing() {
            if (OnStart != null) OnStart(this);

            double endTime = _starTime + _duration;

            _rEasingInfo.CurrentValue = _rEasingInfo.StartValue;
            _gEasingInfo.CurrentValue = _gEasingInfo.StartValue;
            _bEasingInfo.CurrentValue = _bEasingInfo.StartValue;
            _aEasingInfo.CurrentValue = _aEasingInfo.StartValue;

            while (Time.time < endTime) {
                _currentTime = Time.time - _starTime;

                EasingUpdate();
                if (OnUpdate != null) OnUpdate(this);

                yield return 0;
            }
            _currentTime = _duration;
            EasingUpdate();
            if (OnUpdate != null) OnUpdate(this);

            if (OnEnd != null) OnEnd(this);
        }

        protected virtual void EasingUpdate() {
            if (_rEasingInfo.ChangeValue != 0) {
                _rEasingInfo.Update(_currentTime, _duration);
            }
            if (_gEasingInfo.ChangeValue != 0) {
                _gEasingInfo.Update(_currentTime, _duration);
            }
            if (_bEasingInfo.ChangeValue != 0) {
                _bEasingInfo.Update(_currentTime, _duration);
            }
            if (_aEasingInfo.ChangeValue != 0) {
                _aEasingInfo.Update(_currentTime, _duration);
            }
        }
    }
}