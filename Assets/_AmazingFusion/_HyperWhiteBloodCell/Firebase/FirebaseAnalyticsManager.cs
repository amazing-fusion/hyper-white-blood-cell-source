using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCellDash
{
    public class FirebaseAnalyticsManager : GlobalSingleton<FirebaseAnalyticsManager>
    {

        // Use this for initialization
        void Start()
        {
            FirebaseAnalytics.Init();
        }

        public void SendLevel(int level) {
            FirebaseAnalytics.LogEvent(string.Format("Level {0}", level));
        }

        public void SendCrossPromotion(string bannerKey) {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("BannerKey", bannerKey);
            FirebaseAnalytics.LogEvent("Cross Promotion", parameters);
        }

        public void SendRewardedAdverStart() {
            FirebaseAnalytics.LogEvent("Rewarded Ad Started");
        }

        public void SendRewardedAdverWatched() {
            FirebaseAnalytics.LogEvent("Rewarded Ad Watched");
        }

        public void SendIntersticialAdverStart() {
            FirebaseAnalytics.LogEvent("Intersticial Ad Started");
        }
    }
}
