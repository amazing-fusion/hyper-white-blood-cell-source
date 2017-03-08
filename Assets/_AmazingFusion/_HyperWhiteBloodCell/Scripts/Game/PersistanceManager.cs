using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    public class PersistanceManager : GlobalSingleton<PersistanceManager> {

        int _bestLevel;

        public int BestLevel {
            get {
                return _bestLevel;
            }

            set {
                if (value > _bestLevel) {
                    _bestLevel = value;

                    UM_GameServiceManager.Instance.SubmitScore("leaderboard_ranking", _bestLevel);

                    PlayerPrefs.SetInt("BestLevel", _bestLevel);
                    PlayerPrefs.Save();
                }
            }
        }

        // Use this for initialization
        void Start() {
            _bestLevel = PlayerPrefs.GetInt("BestLevel", 0);
        }
    }
}