using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion {
    public class EffectsManager : Singleton<EffectsManager> {

        Queue<IEffectable> _effectsQueue = new Queue<IEffectable>();
        IEffectable _currentEffect = null;

        public void AddEffect(IEffectable effect) {
            if (_currentEffect == null) {
                PlayEffect(effect);
            } else {
                _effectsQueue.Enqueue(effect);
            }
        }

        void EffectEnded(IEffectable effect) {
            effect.OnEnd -= EffectEnded;
            PlayNextEffect();
        }

        void PlayNextEffect() {
            if (_effectsQueue.Count > 0) {
                PlayEffect(_effectsQueue.Dequeue());
            } else {
                _currentEffect = null;
            }
        }

        void PlayEffect(IEffectable effect) {
            _currentEffect = effect;
            _currentEffect.OnEnd += EffectEnded;
            _currentEffect.Play();
        }
    }
}