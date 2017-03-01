using System;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

namespace com.AmazingFusion {
    public class ValueEasingAnimation : EasingAnimation {

        [SerializeField]
        protected EasingInfo _easingInfo;

        public override event Action<IEffectable> OnStart;
        public override event Action<IEffectable> OnUpdate;
        public override event Action<IEffectable> OnEnd;

        public void SetStartValueAsCurrentValue() {
            _easingInfo.StartValue = _easingInfo.CurrentValue;
        }

        public void SetStartValue(double startValue) {
            _easingInfo.StartValue = startValue;
        }

        public void SetChangeValue(double changeValue) {
            _easingInfo.ChangeValue = changeValue;
        }

        public void SetEndValue(double endValue) {
            SetChangeValue(endValue - _easingInfo.StartValue);
        }

        protected override IEnumerator<float> DoEasing() {
            if (OnStart != null) OnStart(this);

            double endTime = _starTime + _duration;
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
            _easingInfo.Update(_currentTime, _duration);
        }
    }
}