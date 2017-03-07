using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace com.AmazingFusion {
    [RequireComponent(typeof(TMP_Text))]
    public class TextColorEasingAnimation : ColorEasingAnimation {

        TMP_Text _text;

        void Awake() {
            _text = GetComponent<TMP_Text>();
        }

        public void SetStartColorAsCurrentColor() {
            _rEasingInfo.StartValue = _text.color.r;
            _gEasingInfo.StartValue = _text.color.g;
            _bEasingInfo.StartValue = _text.color.b;
            _aEasingInfo.StartValue = _text.color.a;
        }


        protected override void EasingUpdate() {
            base.EasingUpdate();
            _text.color = new Color(
                        (float)_rEasingInfo.CurrentValue,
                        (float)_gEasingInfo.CurrentValue,
                        (float)_bEasingInfo.CurrentValue,
                        (float)_aEasingInfo.CurrentValue);
        }
    }
}