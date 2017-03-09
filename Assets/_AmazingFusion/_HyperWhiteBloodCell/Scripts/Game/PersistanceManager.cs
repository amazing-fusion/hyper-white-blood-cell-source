using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    public class PersistanceManager : GlobalSingleton<PersistanceManager> {

        bool _audioOn;

        int _bestLevel;

        int _germsKilled;

        public bool AudioOn {
            get {
                return _audioOn;
            }

            set {
                _audioOn = value;

                PlayerPrefs.SetInt("AudioOn", _audioOn ? 1 : 0);
                PlayerPrefs.Save();
            }
        }

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

        public int GermsKilled {
            get {
                return _germsKilled;
            }

            set {
                if (value > _germsKilled) {
                    _germsKilled = value;

                    //TODO: Achievements

                    PlayerPrefs.SetInt("GermsKilled", _germsKilled);
                    PlayerPrefs.Save();
                }
            }
        }

        // Use this for initialization
        protected override void Awake() {
            base.Awake();
            _audioOn = PlayerPrefs.GetInt("AudioOn", 1) == 1;
            _bestLevel = PlayerPrefs.GetInt("BestLevel", 0);
            _germsKilled = PlayerPrefs.GetInt("GermsKilled", 0);
        }
    }
}