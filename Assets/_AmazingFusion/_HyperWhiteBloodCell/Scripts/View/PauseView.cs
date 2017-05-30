using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCellDash {
    public class PauseView : MonoBehaviour {

        [SerializeField]
        RectTransform _pausePanel;

        // Use this for initialization
        void Start() {
            GameController.Instance.OnPause += Show;
        }

        void OnDestroy() {
            if (GameController.HasInstance) {
                GameController.Instance.OnPause -= Show;
            }
        }

        void Show() {
            _pausePanel.gameObject.SetActive(true);
        }

        public void Hide() {
            _pausePanel.gameObject.SetActive(false);
            GameController.Instance.Resume();
        }
    }
}