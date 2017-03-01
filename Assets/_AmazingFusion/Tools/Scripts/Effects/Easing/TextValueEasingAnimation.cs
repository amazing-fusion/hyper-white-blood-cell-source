using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace com.AmazingFusion {
    [RequireComponent(typeof(TMP_Text))]
    public class TextValueEasingAnimation : ValueEasingAnimation {

        [SerializeField]
        string _stringFormat = "0";

        TMP_Text _text;

        void Awake() {
            _text = GetComponent<TMP_Text>();
        }

        protected override void EasingUpdate() {
            base.EasingUpdate();
            _text.text = _easingInfo.CurrentValue.ToString(_stringFormat);
        }
    }
}