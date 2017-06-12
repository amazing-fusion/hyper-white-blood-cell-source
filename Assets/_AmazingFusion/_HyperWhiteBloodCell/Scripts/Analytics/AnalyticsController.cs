using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCellDash
{
    public class AnalyticsController : GlobalSingleton<AnalyticsController>
    {
        const bool FLURRY_DEBUG_ENABLED = true;
        const string FLURRY_IOS_API_KEY = "MQQ9B9NRQ3VJ3QV3W7C4";
        const string FLURRY_ANDROID_API_KEY = "94QHCDKHR2HKRF6QTDZX";

        // Use this for initialization
        void Start()
        {
#if UNITY_ANDROID
            FirebaseAnalytics.Init();
#endif
            KHD.FlurryAnalytics.Instance.SetDebugLogEnabled(FLURRY_DEBUG_ENABLED);

#if UNITY_IOS
            KHD.FlurryAnalyticsIOS.SetIAPReportingEnabled(true);
#endif

            KHD.FlurryAnalytics.Instance.StartSession(
                FLURRY_IOS_API_KEY,
                FLURRY_ANDROID_API_KEY,
                true);
        }

        public void SendLevel(int level) {
#if UNITY_ANDROID
            FirebaseAnalytics.LogEvent(string.Format("Level {0}", level));
#endif

            KHD.FlurryAnalytics.Instance.LogEvent(string.Format("Level {0}", level));
        }

        public void SendCrossPromotion(string bannerKey) {
#if UNITY_ANDROID
            Dictionary<string, object> firParameters = new Dictionary<string, object>();
            firParameters.Add("BannerKey", bannerKey);
            FirebaseAnalytics.LogEvent("Cross Promotion", firParameters);
#endif

            Dictionary<string, string> flurryParameters = new Dictionary<string, string>();
            flurryParameters.Add("BannerKey", bannerKey);
            KHD.FlurryAnalytics.Instance.LogEventWithParameters("Cross Promotion", flurryParameters);
        }

        public void SendRewardedAdverStart() {
#if UNITY_ANDROID
            FirebaseAnalytics.LogEvent("Rewarded Ad Started");
#endif

            KHD.FlurryAnalytics.Instance.LogEvent("Rewarded Ad Started");
        }

        public void SendRewardedAdverWatched() {
#if UNITY_ANDROID
            FirebaseAnalytics.LogEvent("Rewarded Ad Watched");
#endif

            KHD.FlurryAnalytics.Instance.LogEvent("Rewarded Ad Watched");
        }

        public void SendIntersticialAdverStart() {
#if UNITY_ANDROID
            FirebaseAnalytics.LogEvent("Intersticial Ad Started");
#endif

            KHD.FlurryAnalytics.Instance.LogEvent("Intersticial Ad Started");
        }

        public void SendNotificationClicked(string notificationKey) {
#if UNITY_ANDROID
            Dictionary<string, object> firParameters = new Dictionary<string, object>();
            firParameters.Add("Notification", notificationKey);
            FirebaseAnalytics.LogEvent("Notification Clicked", firParameters);
#endif

            Dictionary<string, string> flurryParameters = new Dictionary<string, string>();
            flurryParameters.Add("Notification", notificationKey);
            KHD.FlurryAnalytics.Instance.LogEventWithParameters("Notification Clicked", flurryParameters);
        }
    }
}
