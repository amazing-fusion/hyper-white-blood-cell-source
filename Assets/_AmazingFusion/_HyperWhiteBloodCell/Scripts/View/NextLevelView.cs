using MovementEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class NextLevelView : Singleton<NextLevelView>
    {
        [SerializeField]
        EasingAnimation _showAnimation;

        [SerializeField]
        EasingAnimation _hideAnimation;

        [SerializeField]
        TMP_Text _levelText;

        [SerializeField]
        EasingAnimation _startLevelAnimation;

        [SerializeField]
        Image _fadeImage;

        public event System.Action OnReviewUI;

        void Start()
        {
            _showAnimation.OnEnd += OnShowAnimationEnd;
            _hideAnimation.OnEnd += OnHideAnimationEnd;

            Room.OnLevelEnd += OnLevelEnd;
        }

        void OnDestroy() {
            _showAnimation.OnEnd -= OnShowAnimationEnd;
            _hideAnimation.OnEnd -= OnHideAnimationEnd;

            Room.OnLevelEnd -= OnLevelEnd;
        }

        void OnShowAnimationEnd(IEffectable effect) {
            LevelManager.Instance.NextLevel();
            Hide();
        }

        void OnHideAnimationEnd(IEffectable effect) {
            _hideAnimation.gameObject.SetActive(false);
            StartLevel();
        }

        void OnStartLevelAnimationEnd(IEffectable effect) {
            _startLevelAnimation.OnEnd -= OnStartLevelAnimationEnd;
            LevelManager.Instance.CurrentRoom.StartLevel();
        }

        void OnLevelEnd(Room room, bool win)
        {
            if (win)
            {
                Show();
            }
            else
            {
                if (AdsController.Instance.AvailableReviewUI &&
                        !PersistanceManager.Instance.NeverReviewUI && 
                        AdsController.Instance.NextReviewUI < Time.time)
                {
                    if (OnReviewUI != null) OnReviewUI();
                    AdsController.Instance.AvailableReviewUI = false;
                }
                else if (PersistanceManager.Instance.ShowAds && AdsController.Instance.NextTimeAds < Time.time)
                {
                    AdsController.Instance.ShowInterstisialAd();
                }
            }
            
        }

        public void Show()
        {
            if (_showAnimation != null) {

                _showAnimation.gameObject.SetActive(true);
                _showAnimation.Play();
            }
        }

        void Hide()
        {
            _hideAnimation.Play();
        }

        public void StartLevel() {
            _levelText.text = string.Format("Level {0}", LevelManager.Instance.CurrentLevelNumber + 1);
            _startLevelAnimation.OnEnd += OnStartLevelAnimationEnd;
            _startLevelAnimation.Play();
            EffectsControllerPlayer.Instance.Initialize();
        }
    }
}

