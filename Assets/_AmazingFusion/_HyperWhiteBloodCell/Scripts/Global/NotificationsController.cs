using UnityEngine;
using System.Collections.Generic;
using EasyEditor;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    public class NotificationsController : Singleton<NotificationsController> {

        [System.Serializable]
        public class TimeNotificationDictionary : SerializableDictionary<int, string[], TimeNotificationPair> { }
        [System.Serializable]
        public class TimeNotificationPair : SerializableKeyValuePair<int, string[]> { }

        [SerializeField, Message(text = "Key:\tTime in seconds.\nValue:\tLocalized text key.", messageType = MessageType.Info)]
        TimeNotificationDictionary _timeNotifications;

        void Start()
        {
            UM_NotificationController.Instance.CancelAllLocalNotifications();
        }

        public void SetNotifications()
        {
            if (UM_NotificationController.HasInstance)
            {
                foreach (KeyValuePair<int, string[]> timeNotification in _timeNotifications)
                {
                    string notificationText = timeNotification.Value[Random.Range(0, timeNotification.Value.Length)];
                    UM_NotificationController.Instance.ScheduleLocalNotification(
                                Application.productName,
                                notificationText,
                                timeNotification.Key);

                    //Debug.Log("<color=blue>" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</color>\n" + System.DateTime.Now.AddSeconds(timeNotification.Key).ToString("yyyy-MM-dd HH:mm:ss") + " => " + notificationText);
                    //GameAnalyticsSDK.GameAnalytics.NewErrorEvent(GameAnalyticsSDK.GAErrorSeverity.Debug, "[NotificationController] " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - Notification scheduled: " + System.DateTime.Now.AddSeconds(timeNotification.Key).ToString("yyyy-MM-dd HH:mm:ss") + " => " + notificationText);
                }
            }
        }

        //void OnDestroy()
        //{
        //    SetNotifications();
        //}

        //void OnApplicationFocus(bool hasFocus)
        //{
        //    if (!hasFocus)
        //    {
        //        SetNotifications();
        //    }
        //    else
        //    {
        //        UM_NotificationController.Instance.CancelAllLocalNotifications();
        //    }
        //}

        protected override void OnApplicationQuit() {
            SetNotifications();
            base.OnApplicationQuit();
        }

        void OnApplicationPause(bool pause) {
            if (pause) {
                SetNotifications();
            } else {
                UM_NotificationController.Instance.CancelAllLocalNotifications();
            }
        }
    }
}