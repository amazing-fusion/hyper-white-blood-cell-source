using UnityEngine;
using System.Collections.Generic;
using EasyEditor;
using DemiumGames;

namespace com.AmazingFusion.HyperWhiteBloodCellDash {
    public class NotificationsController : Singleton<NotificationsController> { 

        [System.Serializable]
        private struct NotificationData {
            [SerializeField]
            string _key;

            [SerializeField]
            string[] _text;

            [SerializeField]
            int _seconds;

            public string Key {
                get {
                    return _key;
                }
            }

            public string[] Text {
                get {
                    return _text;
                }
            }

            public int Seconds {
                get {
                    return _seconds;
                }
            }
        }

        [SerializeField]
        NotificationData[] _notifications;

        [SerializeField]
        DGNotificationManager.DGNotificationCallback _notificationCallBack;

#if !UM_NOTIFICATIONS && UNITY_ANDROID
        protected override void Awake() {
            base.Awake();
            for (int i = 0; i < _notifications.Length; ++i) {
                DemiumGames.DGNotificationManager.Instance.SetCallbackInt(i, _notificationCallBack);
            }
        }
#endif

        void Start()
        {
#if UM_NOTIFICATIONS || UNITY_IOS
            UM_NotificationController.Instance.CancelAllLocalNotifications();
#elif UNITY_ANDROID
            CancelAllNotifications();
            //DGNotificationManager.Instance.CancelAllNotifications();
#endif
        }

        public void SetNotifications()
        {
#if UM_NOTIFICATIONS || UNITY_IOS
            if (UM_NotificationController.HasInstance)
            {
                UM_NotificationController.Instance.CancelAllLocalNotifications();
                foreach (NotificationData notification in _notifications)
                {
                    string notificationText = notification.Text[Random.Range(0, notification.Text.Length)];
                    UM_NotificationController.Instance.ScheduleLocalNotification(
                                Application.productName,
                                notificationText,
                                notification.Seconds);
                }
            }
#elif UNITY_ANDROID
            CancelAllNotifications();
            //DGNotificationManager.Instance.CancelAllNotifications();
            for (int i = 0; i < _notifications.Length; ++i) {
                NotificationData notification = _notifications[i];
                string notificationText = notification.Text[Random.Range(0, notification.Text.Length)];
                DGNotificationManager.Instance.SendNotificationWithAppIcon(
                        i,
                        Application.productName,
                        notificationText,
                        "notificationsicon",
                        notification.Seconds);
            }
#endif

        }

        void CancelAllNotifications() {
            for (int i = 0; i < _notifications.Length; ++i) {
                DGNotificationManager.Instance.CancelNotification(i);
            }
        }

        void OnApplicationPause(bool pause) {
            if (pause) {
                SetNotifications();
            } else {
#if UM_NOTIFICATIONS || UNITY_IOS
                UM_NotificationController.Instance.CancelAllLocalNotifications();
#elif UNITY_ANDROID
                CancelAllNotifications();
                //DGNotificationManager.Instance.CancelAllNotifications();
#endif
            }
        }

        public void OnNotificationCallback(int notificationId) {
            AnalyticsController.Instance.SendStartedFromNotification(_notifications[notificationId].Key);
        }
    }
}