using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        void Start()
        {
            AudioController.Instance.PlayMenuMusic();

            UM_GameServiceManager.Instance.Connect();

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
                Debug.Log("Is connected");
                UM_GameServiceManager.Instance.ShowLeaderBoardUI("leaderboard_ranking");
            } else {
                Debug.Log("Connecting...");
                UM_GameServiceManager.OnConnectionStateChnaged += ShowRankingOnConnectionStateChnaged;
                UM_GameServiceManager.Instance.Connect();
            }
        }

        public void ShowAchievements() {
            if (UM_GameServiceManager.Instance.IsConnected) {
                Debug.Log("Is connected");
                UM_GameServiceManager.Instance.ShowAchievementsUI();
            } else {
                Debug.Log("Connecting...");
                UM_GameServiceManager.OnConnectionStateChnaged += ShowAchievementsOnConnectionStateChnaged;
                UM_GameServiceManager.Instance.Connect();
            }
        }

        void ShowRankingOnConnectionStateChnaged(UM_ConnectionState connectionState) {
            if (connectionState == UM_ConnectionState.CONNECTED) {
                Debug.Log("Connected!");
                UM_GameServiceManager.OnConnectionStateChnaged -= ShowRankingOnConnectionStateChnaged;
                UM_GameServiceManager.Instance.ShowLeaderBoardUI("leaderboard_ranking");
            } else {
                Debug.Log("Ranking state: " + connectionState.ToString());
                //TODO: Show error
            }
        }

        void ShowAchievementsOnConnectionStateChnaged(UM_ConnectionState connectionState) {
            if (connectionState == UM_ConnectionState.CONNECTED) {
                Debug.Log("Connected!");
                UM_GameServiceManager.OnConnectionStateChnaged -= ShowAchievementsOnConnectionStateChnaged;
                UM_GameServiceManager.Instance.ShowAchievementsUI();
            } else {
                Debug.Log("Achievements state: " + connectionState.ToString());
                //TODO: Show error
            }
        }
    }
}


