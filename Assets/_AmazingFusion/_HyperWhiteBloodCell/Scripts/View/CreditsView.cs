using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    public class CreditsView : Singleton<CreditsView> {

        [SerializeField]
        EasingAnimation _showAnimation;

        [SerializeField]
        EasingAnimation _hideAnimation;

        void Awake() {
            _hideAnimation.OnEnd += (IEffectable effect) => {
                _hideAnimation.gameObject.SetActive(false);
            };
        }

        public void Show() {
            _showAnimation.gameObject.SetActive(true);
            _showAnimation.Play();
        }

        public void Hide() {
            _hideAnimation.Play();
        }
    }
}