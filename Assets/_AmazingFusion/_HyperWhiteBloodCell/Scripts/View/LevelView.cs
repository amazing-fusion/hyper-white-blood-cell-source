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

        void Awake()
        {
            Initialize();
            LevelManager.Instance.OnLevelChange += OnLevelChange;
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
            _highScoreText.text = string.Format("BEST: {0}", PersistanceManager.Instance.BestLevel + 1);
        }

        void OnLevelChange()
        {
            _levelText.text = (LevelManager.Instance.CurrentLevelNumber + 1).ToString();
            _highScoreText.text = string.Format("BEST: {0}", PersistanceManager.Instance.BestLevel + 1);
        }

        void OnTimeChange() {
            _levelTimeText.text = string.Format("{0}<size=40>s</size>", Mathf.CeilToInt(GameController.Instance.CurrentLevelTime));
        }
    }
}

