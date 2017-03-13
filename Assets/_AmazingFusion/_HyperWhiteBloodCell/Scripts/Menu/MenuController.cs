using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class MenuController : OptimizedBehaviour
    {
        [SerializeField]
        Image _audioButtonImage;

        [SerializeField]
        Sprite _audioOnSprite;

        [SerializeField]
        Sprite _audioOffSprite;

        [SerializeField]
        Button _noAdsButton;

        [SerializeField]
        TMP_Text _bestLevelText;

        void Start()
        {
            AudioController.Instance.PlayMenuMusic();
            UM_GameServiceManager.Instance.Connect();

            UM_InAppPurchaseManager.Client.OnServiceConnected += (UM_BillingConnectionResult result) => {
                if (result.isSuccess) {
                    PersistanceManager.Instance.ShowAds = !UM_InAppPurchaseManager.Client.IsProductPurchased("iap_no_ads");
                    if (!PersistanceManager.Instance.ShowAds) {
                        _noAdsButton.gameObject.SetActive(false);
                    }
                }
            };

            UM_InAppPurchaseManager.Client.Connect();
            _bestLevelText.text = PersistanceManager.Instance.BestLevel.ToString();
            _audioButtonImage.sprite = PersistanceManager.Instance.AudioOn ? _audioOnSprite : _audioOffSprite;
        }

        public void StartGame()
        {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.GameScene);
        }

        public void ShowCredits() {
            CreditsView.Instance.Show();
        }

        public void SwitchAudio() {
            AudioController.Instance.SwitchAudio();
            _audioButtonImage.sprite = AudioController.Instance.IsOn ? _audioOnSprite : _audioOffSprite;
        }

        public void ShowRanking() {
            if (UM_GameServiceManager.Instance.IsConnected) {
                UM_GameServiceManager.Instance.ShowLeaderBoardUI("leaderboard_ranking");
            } else {
                UM_GameServiceManager.OnConnectionStateChnaged += ShowRankingOnConnectionStateChnaged;
                UM_GameServiceManager.Instance.Connect();
            }
        }

        public void ShowAchievements() {
            if (UM_GameServiceManager.Instance.IsConnected) {
                UM_GameServiceManager.Instance.ShowAchievementsUI();
            } else {
                UM_GameServiceManager.OnConnectionStateChnaged += ShowAchievementsOnConnectionStateChnaged;
                UM_GameServiceManager.Instance.Connect();
            }
        }

        public void PurchaseNoAds() {
            if (PersistanceManager.Instance.ShowAds) {
                if (UM_InAppPurchaseManager.Client.IsConnected) {
                    StartPurchaseFlow();
                } else {
                    UM_InAppPurchaseManager.Client.OnServiceConnected += StartPurchaseFlowOnServiceConnected;
                    UM_InAppPurchaseManager.Client.Connect();
                }
            }
        }

        void ShowRankingOnConnectionStateChnaged(UM_ConnectionState connectionState) {
            if (connectionState == UM_ConnectionState.CONNECTED) {
                UM_GameServiceManager.OnConnectionStateChnaged -= ShowRankingOnConnectionStateChnaged;
                UM_GameServiceManager.Instance.ShowLeaderBoardUI("leaderboard_ranking");
            } else {
                //TODO: Show error
            }
        }

        void ShowAchievementsOnConnectionStateChnaged(UM_ConnectionState connectionState) {
            if (connectionState == UM_ConnectionState.CONNECTED) {
                UM_GameServiceManager.OnConnectionStateChnaged -= ShowAchievementsOnConnectionStateChnaged;
                UM_GameServiceManager.Instance.ShowAchievementsUI();
            } else {
                //TODO: Show error
            }
        }

        void StartPurchaseFlowOnServiceConnected(UM_BillingConnectionResult result) {
            if (result.isSuccess) {
                PersistanceManager.Instance.ShowAds = !UM_InAppPurchaseManager.Client.IsProductPurchased("iap_no_ads");
                if (!PersistanceManager.Instance.ShowAds) {
                    _noAdsButton.gameObject.SetActive(false);
                } else {
                    StartPurchaseFlow();
                }
            }
        }

        void StartPurchaseFlow() {
            UM_InAppPurchaseManager.Client.OnPurchaseFinished += NoAdsPurchaseFinished;
            UM_InAppPurchaseManager.Client.Purchase("iap_no_ads");
        }

        void NoAdsPurchaseFinished(UM_PurchaseResult result) {
            if (result.isSuccess) {
                PersistanceManager.Instance.ShowAds = false;
            }
        }
    }
}