using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class LevelView : OptimizedBehaviour
    {
        [SerializeField]
        TMP_Text _levelText;

        [SerializeField]
        TMP_Text _highScoreText;

        [SerializeField]
        TMP_Text _levelTimeText;

        [SerializeField]
        int _countDownStart;

        [SerializeField]
        EasingAnimation _countDownAnimation;

        [SerializeField]
        TMP_Text _countDownText;

        int _seconds;
        int Seconds {
            get {
                return _seconds;
            }

            set {
                if (value != _seconds) {
                    _seconds = value;
                    if (_seconds <= _countDownStart) {
                        _countDownText.text = _seconds == 0 ? "Time over" : _seconds.ToString();
                        _countDownAnimation.Play();
                    }
                }
            }
        }

        void Awake()
        {
            Initialize();
            LevelManager.Instance.OnLevelChange += OnLevelChange;
            PersistanceManager.Instance.OnBestLevelChanged += OnBestLevelChange;
            GameController.Instance.OnTimeChange += OnTimeChange;
        }

        void OnDestroy() {
            if (LevelManager.HasInstance) {
                LevelManager.Instance.OnLevelChange -= OnLevelChange;
            }
            if (GameController.HasInstance) {
                GameController.Instance.OnTimeChange -= OnTimeChange;
            }
        }

        void Initialize()
        {
            _levelText.text = (LevelManager.Instance.CurrentLevelNumber + 1).ToString();
            _highScoreText.text = string.Format("High: {0}", PersistanceManager.Instance.BestLevel);
        }

        void OnLevelChange()
        {
            _levelText.text = (LevelManager.Instance.CurrentLevelNumber + 1).ToString();
        }

        void OnBestLevelChange() {
            _highScoreText.text = string.Format("High: {0}", PersistanceManager.Instance.BestLevel);
        }

        void OnTimeChange() {
            int seconds = Mathf.CeilToInt(GameController.Instance.CurrentLevelTime);
            if (seconds <= _countDownStart && seconds != _seconds) {
                _seconds = seconds;
                _countDownText.text = _seconds == 0 ? "Time over" : _seconds.ToString();
                _countDownAnimation.Play();
            }

            _levelTimeText.text = string.Format("{0}<size=40>s</size>", seconds);
        }
    }
}

