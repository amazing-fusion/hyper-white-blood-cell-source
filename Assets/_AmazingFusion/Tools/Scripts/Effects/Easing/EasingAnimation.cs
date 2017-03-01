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

        public abstract event Action<IEffectable> OnStart;
        public abstract event Action<IEffectable> OnUpdate;
        public abstract event Action<IEffectable> OnEnd;


        public void Play() {
            _starTime = Time.time;
            Timing.RunCoroutine(DoEasing());
        }

        public CoroutineHandle PlayCoroutine() {
            _starTime = Time.time;
            return Timing.RunCoroutine(DoEasing());
        }

        protected abstract IEnumerator<float> DoEasing();
    }
}