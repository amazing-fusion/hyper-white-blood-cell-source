using System;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

namespace com.AmazingFusion {
    public abstract class EasingAnimation : OptimizedBehaviour, IEffectable {

        [SerializeField]
        protected double _duration;

        protected double _starTime;
        protected double _currentTime;

        protected CoroutineHandle _coroutine;

        public abstract event Action<IEffectable> OnStart;
        public abstract event Action<IEffectable> OnUpdate;
        public abstract event Action<IEffectable> OnEnd;

        void OnDestroy() {
            if (_coroutine != null) {
                Timing.KillCoroutines(_coroutine);
            }
        }

        public void Play() {
            _coroutine = PlayCoroutine();
        }

        public CoroutineHandle PlayCoroutine() {
            _starTime = Time.time;
            OnEnd += (IEffectable effect) => { _coroutine = null; };
            return Timing.RunCoroutine(DoEasing());
        }

        protected abstract IEnumerator<float> DoEasing();
    }
}