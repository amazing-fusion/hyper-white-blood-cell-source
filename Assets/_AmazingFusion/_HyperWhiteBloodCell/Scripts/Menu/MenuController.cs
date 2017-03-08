using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class MenuController : OptimizedBehaviour
    {
        void Start()
        {
            AudioController.Instance.PlayMenuMusic();
            UM_GameServiceManager.Instance.Connect();
        }

        public void StartGame()
        {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.GameScene);
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
    }
}


