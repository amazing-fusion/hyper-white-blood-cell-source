using System;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

namespace com.AmazingFusion {
    public class ScaleVector3EasingAnimation : Vector3EasingAnimation {

        protected override void EasingUpdate() {
            base.EasingUpdate();
            Transform.localScale = new Vector3(
                        (float)_xEasingInfo.CurrentValue,
                        (float)_yEasingInfo.CurrentValue,
                        (float)_zEasingInfo.CurrentValue);
        }
    }
}