using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCellDash
{
    public class AnalyticsController : GlobalSingleton<AnalyticsController>
    {
        const bool FLURRY_DEBUG_ENABLED = true;
        const string FLURRY_IOS_API_KEY = "PD2FSP3DMP3PN3MKFFFS";
        const string FLURRY_ANDROID_API_KEY = "4NXSHC576ZGFKD2BPNM9";

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
                FLURRY_IOS_API_KEY, // - replace with your API KEY.
                FLURRY_ANDROID_API_KEY, // - replace with your API KEY.
                true);
        }

        public void SendLevel(int level) {
#if UNITY_ANDROID
            FirebaseAnalytics.LogEvent(string.Format("Level {0}", level));
#endif

            //TODO: probar a lanzar con parámetros (ver funnel)
            //KHD.FlurryAnalytics.Instance.LogEvent(string.Format("Level {0}", level));

            Dictionary<string, string> flurryParameters = new Dictionary<string, string>();
            flurryParameters.Add("Level", level.ToString());
            KHD.FlurryAnalytics.Instance.LogEventWithParameters("Level Reached", flurryParameters);
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
    }
}
