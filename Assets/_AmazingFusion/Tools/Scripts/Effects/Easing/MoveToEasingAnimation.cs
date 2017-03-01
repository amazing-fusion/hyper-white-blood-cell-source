using System;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

namespace com.AmazingFusion {
    public class MoveToEasingAnimation : Vector3EasingAnimation {

        public void SetStartPosition(Vector3 startPosition) {
            _xEasingInfo.StartValue = startPosition.x;
            _yEasingInfo.StartValue = startPosition.y;
            _zEasingInfo.StartValue = startPosition.z;
        }

        public void SetStartPositionAsCurrentLocalPosition() {
            SetStartPosition(Transform.localPosition);
        }

        public void SetChangePosition(Vector3 changePosition) {
            _xEasingInfo.ChangeValue = changePosition.x;
            _yEasingInfo.ChangeValue = changePosition.y;
            _zEasingInfo.ChangeValue = changePosition.z;
        }

        public void SetEndPosition(Vector3 endPosition) {
            _xEasingInfo.ChangeValue = endPosition.x - (float)_xEasingInfo.StartValue;
            _yEasingInfo.ChangeValue = endPosition.y - (float)_yEasingInfo.StartValue;
            _zEasingInfo.ChangeValue = endPosition.z - (float)_zEasingInfo.StartValue;
        }

        public void SetEndPositionAsCurrentLocalPosition() {
            SetEndPosition(Transform.localPosition);
        }

        protected override void EasingUpdate() {
            base.EasingUpdate();
            Transform.localPosition = new Vector3(
                        (float)_xEasingInfo.CurrentValue,
                        (float)_yEasingInfo.CurrentValue,
                        (float)_zEasingInfo.CurrentValue);
        }
    }
}