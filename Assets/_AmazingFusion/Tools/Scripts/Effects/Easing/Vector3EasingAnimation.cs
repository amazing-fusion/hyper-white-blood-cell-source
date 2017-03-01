using System;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

namespace com.AmazingFusion {
    public class Vector3EasingAnimation : EasingAnimation {

        [SerializeField]
        protected EasingInfo _xEasingInfo;

        [SerializeField]
        protected EasingInfo _yEasingInfo;

        [SerializeField]
        protected EasingInfo _zEasingInfo;

        public override event Action<IEffectable> OnStart;
        public override event Action<IEffectable> OnUpdate;
        public override event Action<IEffectable> OnEnd;

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
            if (_xEasingInfo.ChangeValue != 0) {
                _xEasingInfo.Update(_currentTime, _duration);
            }
            if (_yEasingInfo.ChangeValue != 0) {
                _yEasingInfo.Update(_currentTime, _duration);
            }
            if (_zEasingInfo.ChangeValue != 0) {
                _zEasingInfo.Update(_currentTime, _duration);
            }
        }
    }
}