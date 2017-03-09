using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    public class TutorialView : Singleton<TutorialView> {

        [SerializeField]
        Image _triggerImage;

        [SerializeField]
        EasingAnimation _hideAnimation;

        void Start() {
            _hideAnimation.OnEnd += HideAnimationEnd;
        }

        void OnDestroy() {
            _hideAnimation.OnEnd -= HideAnimationEnd;
        }

        public void Hide() {
            _triggerImage.raycastTarget = false;
            _hideAnimation.Play();
        }

        void HideAnimationEnd(IEffectable effect) {
            Destroy(_triggerImage.gameObject);
            NextLevelView.Instance.StartLevel();
        }
    }
}