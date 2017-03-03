using System;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;


namespace com.AmazingFusion {
    public class MultiEasingAnimation : EasingAnimation {

        [SerializeField]
        EasingAnimation[] _easingAnimations;

        public override event Action<IEffectable> OnEnd;
        public override event Action<IEffectable> OnStart;
        public override event Action<IEffectable> OnUpdate;

        int _left;

        protected override IEnumerator<float> DoEasing() {
            if (OnStart != null) OnStart(this);

            _left = _easingAnimations.Length;

            foreach (EasingAnimation animation in _easingAnimations) {
                animation.Play();
                animation.OnEnd += OnEasingEnd;
            }

            yield return 0;
        }

        void OnEasingEnd(IEffectable animation) {
            --_left;
            animation.OnEnd -= OnEasingEnd;
            if (OnUpdate != null) OnUpdate(this);
            if (_left <= 0) {
                if (OnEnd != null) OnEnd(this);
            }
        }
    }
}