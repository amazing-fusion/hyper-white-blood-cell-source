using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class GameOverView : OptimizedBehaviour
    {
        [SerializeField]
        TMP_Text _deathText;

        [SerializeField]
        TMP_Text _levelText;

        [SerializeField]
        TMP_Text _bestLevelText;

        [SerializeField]
        AlphaCanvasGroupEasingAnimation _showAnimation;

        [SerializeField]
        AlphaCanvasGroupEasingAnimation _hideAnimation;

        void Awake()
        {
            GameController.Instance.OnPlayerDied += OnPlayerDie;
            GameController.Instance.OnTimeOver += OnTimeOver;
        }
        
        void Start()
        {
            _showAnimation.OnEnd += (IEffectable effect) => {
                _showAnimation.CanvasGroup.interactable = true;
            };

            _hideAnimation.OnEnd += (IEffectable effect) => {
                _hideAnimation.gameObject.SetActive(false);
                GameController.Instance.RestartGame();
            };
        }

        void OnPlayerDie()
        {
            _deathText.text = "You're infected!";
            _levelText.text = string.Format("Level {0}", (LevelManager.Instance.CurrentLevelNumber + 1));
            _bestLevelText.text = string.Format("Best {0}", PersistanceManager.Instance.BestLevel + 1);
            _showAnimation.gameObject.SetActive(true);
            _showAnimation.Play();
        }

        void OnTimeOver() {
            _deathText.text = "Time's\nup!";
            _levelText.text = string.Format("Level {0}", (LevelManager.Instance.CurrentLevelNumber + 1));
            _bestLevelText.text = string.Format("Best {0}", PersistanceManager.Instance.BestLevel + 1);
            _showAnimation.gameObject.SetActive(true);
            _showAnimation.Play();
        }

        public void RestartGame()
        {
            _hideAnimation.CanvasGroup.interactable = false;
            _hideAnimation.Play();
        }

        public void GoToMenu()
        {
            _hideAnimation.CanvasGroup.interactable = false;
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.MenuScene);
        }
    }
}

