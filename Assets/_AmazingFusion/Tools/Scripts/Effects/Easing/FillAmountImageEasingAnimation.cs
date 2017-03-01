using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.AmazingFusion {
    [RequireComponent(typeof(Image))]
    public class FillAmountImageEasingAnimation : ValueEasingAnimation {

        [SerializeField]
        float _maxValue = 1f;

        Image _image;

        public float MaxValue {
            get {
                return _maxValue;
            }

            set {
                _maxValue = value;
            }
        }

        void Awake() {
            _image = GetComponent<Image>();
        }

        protected override void EasingUpdate() {
            base.EasingUpdate();
            _image.fillAmount = (float) _easingInfo.CurrentValue / _maxValue;
        }
    }
}