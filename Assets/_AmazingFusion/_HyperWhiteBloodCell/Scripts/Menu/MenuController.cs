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
                UM_GameServiceManager.Instance.ShowLeaderBoardUI("ranking");
            } else {
                UM_GameServiceManager.OnPlayerConnected += ShowRankingOnConnect;
                UM_GameServiceManager.Instance.Connect();
            }
        }

        void ShowRankingOnConnect() {
            UM_GameServiceManager.OnPlayerConnected -= ShowRankingOnConnect;
            UM_GameServiceManager.Instance.ShowLeaderBoardUI("ranking");
        }
    }
}


