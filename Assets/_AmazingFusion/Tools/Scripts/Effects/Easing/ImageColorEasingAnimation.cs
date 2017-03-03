using UnityEngine;
using UnityEngine.UI;

namespace com.AmazingFusion {
    [RequireComponent(typeof(Image))]
    public class ImageColorEasingAnimation : ColorEasingAnimation {

        Image _image;

        void Awake() {
            _image = GetComponent<Image>();
        }

        public void SetStartColorAsCurrentColor() {
            _rEasingInfo.StartValue = _image.color.r;
            _gEasingInfo.StartValue = _image.color.g;
            _bEasingInfo.StartValue = _image.color.b;
            _aEasingInfo.StartValue = _image.color.a;
        }


        protected override void EasingUpdate() {
            base.EasingUpdate();
            _image.color = new Color(
                        (float)_rEasingInfo.CurrentValue,
                        (float)_gEasingInfo.CurrentValue,
                        (float)_bEasingInfo.CurrentValue,
                        (float)_aEasingInfo.CurrentValue);

            Debug.Log(_image.color);
        }
    }
}