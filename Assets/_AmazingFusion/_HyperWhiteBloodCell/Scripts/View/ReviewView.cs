﻿using System.Collections;
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
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.AmazingFusion.HyperWhiteBloodCellDash");
#elif UNITY_IOS
            Application.OpenURL("https://itunes.apple.com/us/app/hyper-white-blood-cell-dash/id1214605681?ls=1&mt=8");
#endif 
           
        }
        
    }
}
