using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    public class PersistanceManager : GlobalSingleton<PersistanceManager> {

        bool _neverReviewUI;

        bool _audioOn;

        int _bestLevel;

        int _germsKilled;

        bool _showAds;

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
                    int levelNumber = _bestLevel + 1;

                    UM_GameServiceManager.Instance.SubmitScore("leaderboard_ranking", levelNumber);

                    if (levelNumber >= 5) {
                        UM_GameServiceManager.Instance.UnlockAchievement("achievement_reach_level_5");
                    }
                    if (levelNumber >= 10) {
                        UM_GameServiceManager.Instance.UnlockAchievement("achievement_reach_level_10");
                    }
                    if (levelNumber >= 15) {
                        UM_GameServiceManager.Instance.UnlockAchievement("achievement_reach_level_15");
                    }
                    if (levelNumber >= 50) {
                        UM_GameServiceManager.Instance.UnlockAchievement("achievement_reach_level_50");
                    }
                    if (levelNumber >= 100) {
                        UM_GameServiceManager.Instance.UnlockAchievement("achievement_reach_level_0");
                    }

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
                    float killed = value - _germsKilled;

                    _germsKilled = value;

                    if (UM_GameServiceManager.Instance.GetAchievementProgressInSteps("achievement_defeat_10_germs") < 10) {
                        UM_GameServiceManager.Instance.IncrementAchievement("achievement_defeat_10_germs", killed * 10f);
                    }
                    if (UM_GameServiceManager.Instance.GetAchievementProgressInSteps("achievement_defeat_100_germs") < 100) {
                        UM_GameServiceManager.Instance.IncrementAchievement("achievement_defeat_100_germs", killed * 1f);
                    }
                    if (UM_GameServiceManager.Instance.GetAchievementProgressInSteps("achievement_defeat_500_germs") < 500) {
                        UM_GameServiceManager.Instance.IncrementAchievement("achievement_defeat_500_germs", killed * 0.2f);
                    }
                    if (UM_GameServiceManager.Instance.GetAchievementProgressInSteps("achievement_defeat_1000_germs") < 1000) {
                        UM_GameServiceManager.Instance.IncrementAchievement("achievement_defeat_1000_germs", killed * 0.1f);
                    }
                    if (UM_GameServiceManager.Instance.GetAchievementProgressInSteps("achievement_defeat_10000_germs") < 10000) {
                        UM_GameServiceManager.Instance.IncrementAchievement("achievement_defeat_10000_germs", killed * 0.01f);
                    }

                    PlayerPrefs.SetInt("GermsKilled", _germsKilled);
                    PlayerPrefs.Save();
                }
            }
        }

        public bool ShowAds {
            get {
                return _showAds;
            }

            set {
                _showAds = value;

                PlayerPrefs.SetInt("ShowAds", _showAds ? 1 : 0);
                PlayerPrefs.Save();
            }
        }

        public bool NeverReviewUI
        {
            get
            {
                return _neverReviewUI;
            }

            set
            {
                _neverReviewUI = value;

                PlayerPrefs.SetInt("NeverReviewUI", _neverReviewUI ? 1 : 0);
                PlayerPrefs.Save();
            }
        }

        // Use this for initialization
        protected override void Awake() {
            base.Awake();

            _showAds = PlayerPrefs.GetInt("ShowAds", 1) == 1;
            _neverReviewUI = PlayerPrefs.GetInt("NeverReviewUI", 0) == 1;
            _audioOn = PlayerPrefs.GetInt("AudioOn", 1) == 1;
            _bestLevel = PlayerPrefs.GetInt("BestLevel", 0);
            _germsKilled = PlayerPrefs.GetInt("GermsKilled", 0);
        }
    }
}