using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace com.AmazingFusion.HyperWhiteBloodCellDash {
    public class CreditsView : Singleton<CreditsView> {

        [SerializeField]
        TMP_Text _versionText;

        [SerializeField]
        EasingAnimation _showAnimation;

        [SerializeField]
        EasingAnimation _hideAnimation;

        void Start() {
            _versionText.text = string.Format("v. {0}", Application.version);

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