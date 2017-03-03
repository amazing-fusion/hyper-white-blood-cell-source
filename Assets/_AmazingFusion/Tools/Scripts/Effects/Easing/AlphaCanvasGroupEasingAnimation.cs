using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion {
    [RequireComponent(typeof(CanvasGroup))]
    public class AlphaCanvasGroupEasingAnimation : ValueEasingAnimation {

        CanvasGroup _canvasGroup;
        public CanvasGroup CanvasGroup {
            get {
                if (_canvasGroup == null) {
                    _canvasGroup = GetComponent<CanvasGroup>();
                }
                return _canvasGroup;
            }
        }

        void Awake() {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetStartAlphaAsCurrentAlpha() {
            _easingInfo.StartValue = _canvasGroup.alpha;
        }


        protected override void EasingUpdate() {
            base.EasingUpdate();
           _canvasGroup.alpha = (float)_easingInfo.CurrentValue;
        }
    }
}