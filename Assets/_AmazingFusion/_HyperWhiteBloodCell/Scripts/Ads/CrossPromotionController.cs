using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCellDash {
    public class CrossPromotionController : Singleton<CrossPromotionController> {

        //TODO: Scale for more games
        const string GOOGLE_PLAY_URL = "https://play.google.com/store/apps/details?id=com.KoronStudios.Rabbit.Mercenary.Idle.Clicker";
        const string ITUNES_URL = "https://itunes.apple.com/us/app/rabbit-mercenary-idle-clicker/id1237555963?ls=1&mt=8";

        //[SerializeField]
        //Sprite _promotionSprite; //TODO: Make array

        //[SerializeField]
        //Image _promotionImage;

        [SerializeField]
        RectTransform _promotionPanel;

        string _currentAd;

        public void Show() {
            _promotionPanel.gameObject.SetActive(true);
        }

        public void Hide() {
            _promotionPanel.gameObject.SetActive(false);
        }

        public void GoToLink() {
#if UNITY_ANDROID
            AnalyticsController.Instance.SendCrossPromotion("RabbitMercenaryIdleClicker_01");
            Application.OpenURL(GOOGLE_PLAY_URL);
#elif UNITY_IOS
            AnalyticsController.Instance.SendCrossPromotion("RabbitMercenaryIdleClicker_01");
            Application.OpenURL(ITUNES_URL);
#endif 
            Hide();
        }
    }
}