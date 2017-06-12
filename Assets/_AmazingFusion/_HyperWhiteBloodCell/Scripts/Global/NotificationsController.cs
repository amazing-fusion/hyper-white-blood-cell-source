using UnityEngine;
using System.Collections.Generic;
using EasyEditor;

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

        protected override void Awake() {
            base.Awake();
            for (int i = 0; i < _notifications.Length; ++i) {
                DemiumGames.DGNotificationManager.Instance.SetCallbackInt(i, NotificationCallback);
            }
        }

        void Start()
        {
#if UNITY_IOS
            UM_NotificationController.Instance.CancelAllLocalNotifications();
#elif UNITY_ANDROID

#endif
        }

        public void SetNotifications()
        {
#if UNITY_IOS
            if (UM_NotificationController.HasInstance)
            {
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
            for (int i = 0; i < _notifications.Length; ++i) {
                NotificationData notification = _notifications[i];
                string notificationText = notification.Text[Random.Range(0, notification.Text.Length)];
                DemiumGames.DGNotificationManager.Instance.SendNotificationWithAppIcon(
                        i,
                        Application.productName,
                        notificationText,
                        DemiumGames.Resources.notification_icon,
                        notification.Seconds);
            }
#endif

        }


        private void CancelAllNotifications() {
            for (int i = 0; i < _notifications.Length; i++) {
                for (int j = 0; j < _notifications[i].Text.Length; j++) {
                DemiumGames.DGNotificationManager.Instance.CancelNotification(i,
                    Application.productName,
                    _notifications[i].Text[j],
                    DemiumGames.Resources.notification_icon,
                    "");
                }
            }
        }
        void OnApplicationPause(bool pause) {
            if (pause) {
                SetNotifications();
            } else {
#if UNITY_IOS
                UM_NotificationController.Instance.CancelAllLocalNotifications();
#elif UNITY_ANDROID
                CancelAllNotifications(); 
#endif
            }
        }

        void NotificationCallback(int notificationId) {
            AnalyticsController.Instance.SendNotificationClicked(_notifications[notificationId].Key);
        }
    }
}