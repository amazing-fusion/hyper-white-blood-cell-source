using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class AdsController : Singleton<AdsController>
    {
        [SerializeField]
        float _timeAds;

        [SerializeField]
        float _timeReview;

        float _nextTimeAds, _nextReviewUI;

        bool _availableReviewUI;
        bool _neverReviewUI;

        public float NextTimeAds
        {
            get
            {
                return _nextTimeAds;
            }

            set
            {
                _nextTimeAds = value;
            }
        }

        public float NextReviewUI
        {
            get
            {
                return _nextReviewUI;
            }

            set
            {
                _nextReviewUI = value;
            }
        }

        public bool AvailableReviewUI
        {
            get
            {
                return _availableReviewUI;
            }

            set
            {
                _availableReviewUI = value;
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
            }
        }

        void Start()
        {
            _nextTimeAds = Time.time + (_timeAds);
            _nextReviewUI = Time.time + (_timeReview);
            _availableReviewUI = true;

            GoogleMobileAd.Init();

            GoogleMobileAd.OnInterstitialLoaded += OnInterstisialsLoaded;
            GoogleMobileAd.OnInterstitialOpened += OnInterstisialsOpen;
            GoogleMobileAd.OnInterstitialClosed += OnInterstisialsClosed;

            GoogleMobileAd.OnRewardedVideoLoaded += HandleOnRewardedVideoLoaded;
            GoogleMobileAd.OnRewardedVideoAdClosed += HandleOnRewardedVideoAdClosed;
            

            GoogleMobileAd.LoadRewardedVideo();
            GoogleMobileAd.LoadInterstitialAd();
        }

        void OnDestroy()
        {
            GoogleMobileAd.OnInterstitialLoaded -= OnInterstisialsLoaded;
            GoogleMobileAd.OnInterstitialOpened -= OnInterstisialsOpen;
            GoogleMobileAd.OnInterstitialClosed -= OnInterstisialsClosed;

            GoogleMobileAd.OnRewardedVideoLoaded -= HandleOnRewardedVideoLoaded;
            GoogleMobileAd.OnRewardedVideoAdClosed -= HandleOnRewardedVideoAdClosed;
            GoogleMobileAd.OnRewardedVideoAdLeftApplication -= HandleOnRewardedVideoLeftAplication;
            GoogleMobileAd.OnRewardedVideoAdOpened -= HandleOnRewardedVideoAdOpened;
            GoogleMobileAd.OnRewardedVideoStarted -= HandleOnRewardedVideoAdStarted;

        }

        void HandleOnRewardedVideoLoaded()
        {
            Debug.Log("Load Video Rewarded");
        }

        void HandleOnRewardedVideoAdOpened()
        {
            Debug.Log("Opened Video Rewarded");
        }

        void HandleOnRewardedVideoAdStarted()
        {
            Debug.Log("Started Video Rewarded");
        }

        void HandleOnRewardedVideoLeftAplication()
        {
            Debug.Log("Left App Video Rewarded");
        }

        void HandleOnRewardedVideoAdClosed()
        {
            Debug.Log("Closed Video Rewarded");
        }

        void OnInterstisialsLoaded()
        {
            //ad loaded, strting ad
        }

        void OnInterstisialsOpen()
        {
            //pausing the game
        }

        void OnInterstisialsClosed()
        {
            //un-pausing the game
        }

        public void ShowRewardedVideoAd()
        {
            GoogleMobileAd.ShowRewardedVideo();
            GoogleMobileAd.LoadRewardedVideo();
        }

        public void ShowInterstisialAd()
        {
            GoogleMobileAd.ShowInterstitialAd();
            GoogleMobileAd.LoadInterstitialAd();
            _nextTimeAds = Time.time + (_timeAds * 60);
        }
    }
}

