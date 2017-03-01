using System;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;


namespace com.AmazingFusion {
    public class SequenceEasingAnimation : EasingAnimation {

        [SerializeField]
        EasingAnimation[] _easingAnimations;

        public override event Action<IEffectable> OnEnd;
        public override event Action<IEffectable> OnStart;
        public override event Action<IEffectable> OnUpdate;

        protected override IEnumerator<float> DoEasing() {
            if (OnStart != null) OnStart(this);

            foreach (EasingAnimation animation in _easingAnimations) {
                yield return Timing.WaitUntilDone(animation.PlayCoroutine());
                if (OnUpdate != null) OnUpdate(this);
            }

            if (OnEnd != null) OnEnd(this);
        }
    }
}