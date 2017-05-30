using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCellDash {
    public class CreditsView : Singleton<CreditsView> {

        [SerializeField]
        EasingAnimation _showAnimation;

        [SerializeField]
        EasingAnimation _hideAnimation;

        void Start() {
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