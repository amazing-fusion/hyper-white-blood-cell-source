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
                UM_GameServiceManager.Instance.ShowLeaderBoardUI("leaderboard_ranking");
            } else {
                UM_GameServiceManager.OnConnectionStateChnaged += ShowRankingOnConnectionStateChnaged;
                UM_GameServiceManager.Instance.Connect();
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
    }
}


