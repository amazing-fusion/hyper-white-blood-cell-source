using UnityEngine;

namespace com.AmazingFusion {
    public class ScaleEasingAnimation : ValueEasingAnimation {

        public void SetStartScaleAsCurrentScale()
        {
            _easingInfo.StartValue = Transform.localScale.x;
        }


        protected override void EasingUpdate() {
            base.EasingUpdate();
            float scale = (float)_easingInfo.CurrentValue;
            Transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}