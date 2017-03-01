using System;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

namespace com.AmazingFusion {
    public class MultiEasingAnimation : EasingAnimation {

        [SerializeField]
        EasingInfo[] _easingInfo;

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
            for (int i = 0; i < _easingInfo.Length; ++i) {
                EasingUpdate(i);
            }
        }

        public virtual void EasingUpdate(int easingInfoIndex) {
            _easingInfo[easingInfoIndex].Update(_currentTime, _duration);
        }
    }
}