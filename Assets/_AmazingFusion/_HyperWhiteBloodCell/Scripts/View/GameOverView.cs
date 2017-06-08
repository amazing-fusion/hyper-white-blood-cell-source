using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


namespace com.AmazingFusion.HyperWhiteBloodCellDash
{
    public class GameOverView : OptimizedBehaviour
    {
        [SerializeField]
        TMP_Text _deathText;

        [SerializeField]
        TMP_Text _deathText1;

        [SerializeField]
        TMP_Text _timeText;

        [SerializeField]
        TMP_Text _timeText1;

        [SerializeField]
        TMP_Text _scoreText;

        [SerializeField]
        TMP_Text _bestScoreText;

        [SerializeField]
        Button _videoButton;

        [SerializeField]
        AlphaCanvasGroupEasingAnimation _showAnimation;

        [SerializeField]
        AlphaCanvasGroupEasingAnimation _hideAnimation;

        
        void Start()
        {
            GameController.Instance.OnPlayerDied += OnPlayerDie;
            GameController.Instance.OnTimeOver += OnTimeOver;
            _showAnimation.OnEnd += OnShowAnimationEnd;
            _hideAnimation.OnEnd += OnHideAnimationEnd;
            GoogleMobileAd.OnRewarded += HandleOnRewarded;
        }

        void OnDestroy() {
            if (GameController.HasInstance) {
                GameController.Instance.OnPlayerDied -= OnPlayerDie;
                GameController.Instance.OnTimeOver -= OnTimeOver;
            }
            GoogleMobileAd.OnRewarded -= HandleOnRewarded;
            _showAnimation.OnEnd -= OnShowAnimationEnd;
            _hideAnimation.OnEnd -= OnHideAnimationEnd;
        }

        void OnShowAnimationEnd(IEffectable effect) {
            _showAnimation.CanvasGroup.interactable = true;
        }

        void OnHideAnimationEnd(IEffectable effect) {
            _hideAnimation.gameObject.SetActive(false);
            GameController.Instance.RestartGame();
        }

        void OnPlayerDie()
        {
            if (LevelManager.Instance.AvailableVideo)
            {
                _videoButton.interactable = true;
            }
            else
            {
                _videoButton.interactable = false;
            }
            _deathText.enabled = true;
            _deathText1.enabled = true;
            _timeText.enabled = false;
            _timeText1.enabled = false;
            _deathText.text = "You're";
            _deathText1.text = "infected!";
           _scoreText.text = string.Format("Score {0}", (LevelManager.Instance.CurrentLevelNumber + 1));
            _bestScoreText.text = string.Format("High {0}", PersistanceManager.Instance.BestLevel);
            _showAnimation.gameObject.SetActive(true);
            _showAnimation.Play();
        }

        void OnTimeOver() {
            

            if (LevelManager.Instance.AvailableVideo)
            {
                _videoButton.interactable = true;
            }
            else
            {
                _videoButton.interactable = false;
            }

            _deathText.enabled = false;
            _deathText1.enabled = false;
            _timeText.enabled = true;
            _timeText1.enabled = true;
            _timeText.text = "Time's";
            _timeText1.text = "up!";
            _scoreText.text = string.Format("Score {0}", (LevelManager.Instance.CurrentLevelNumber + 1));
            _bestScoreText.text = string.Format("Best {0}", PersistanceManager.Instance.BestLevel);
            _showAnimation.gameObject.SetActive(true);
            _showAnimation.Play();
        }

        public void RestartGame()
        {
            _hideAnimation.CanvasGroup.interactable = false;
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.GameScene);
            //_hideAnimation.Play();
        }

        public void GoToMenu()
        {
            _hideAnimation.CanvasGroup.interactable = false;
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.MenuScene);
        }

        public void GoToAddVideo()
        {
            AdsController.Instance.ShowRewardedVideoAd();
        }

        void HandleOnRewarded(string itemId, int amount)
        {
            AnalyticsController.Instance.SendRewardedAdverWatched();
            try {
                _hideAnimation.gameObject.SetActive(false);
                LevelManager.Instance.RestartLevel = true;
                GameController.Instance.Revive();
            }
            catch (System.Exception ex) {
                Debug.LogError(ex.Message);
                Debug.LogError(ex.StackTrace);
            }
        }
    }
}

