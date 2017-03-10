using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class ReviewView : OptimizedBehaviour
    {
        [SerializeField]
        TMP_Text _rateUsText;

        [SerializeField]
        TMP_Text _messageText;

        [SerializeField]
        TMP_Text _numberText;

        [SerializeField]
        TMP_Text _starsText;
        
        [SerializeField]
        AlphaCanvasGroupEasingAnimation _showAnimation;

        [SerializeField]
        AlphaCanvasGroupEasingAnimation _hideAnimation;


        void Start()
        {
            NextLevelView.Instance.OnReviewUI += OnReviewView;
            _showAnimation.OnEnd += OnShowAnimationEnd;
            _hideAnimation.OnEnd += OnHideAnimationEnd;
        }

        void OnDestroy()
        {
            if (NextLevelView.HasInstance)
            {
                NextLevelView.Instance.OnReviewUI -= OnReviewView;
            }
            _showAnimation.OnEnd -= OnShowAnimationEnd;
            _hideAnimation.OnEnd -= OnHideAnimationEnd;
        }

        void OnShowAnimationEnd(IEffectable effect)
        {
            _showAnimation.CanvasGroup.interactable = true;
        }

        void OnHideAnimationEnd(IEffectable effect)
        {
            _hideAnimation.gameObject.SetActive(false);
        }

        void OnReviewView()
        {
            _rateUsText.enabled = true;
            _messageText.enabled = true;
            _numberText.enabled = false;
            _starsText.enabled = false;
            _showAnimation.gameObject.SetActive(true);
            _showAnimation.Play();
        }

        public void GoToNotNow()
        {
            _hideAnimation.Play();
            AdsController.Instance.AvailableReviewUI = false;
        }

        public void GoToNever()
        {
            _hideAnimation.Play();
            PersistanceManager.Instance.NeverReviewUI = true;
            AdsController.Instance.AvailableReviewUI = false;
        }

        public void GoToRateUs()
        {
            _hideAnimation.Play();
            AdsController.Instance.AvailableReviewUI = false;

#if UNITY_ANDROID
            Application.OpenURL("http://unity3d.com/");
#elif UNITY_IOS
            Application.OpenURL("");
#endif 
           
        }
        
    }
}
