using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCellDash {
    public class AdsController : Singleton<AdsController>
    {
        [SerializeField]
        float _timeAds;

		[SerializeField]
		float _timeReview;

        [SerializeField]
        float _adLoadTimeout;

        float _nextTimeAds, _nextReviewUI;

        bool _availableReviewUI;
        bool _neverReviewUI;

		float _rewardedAdTimeout;
        float _intersticialAdTimeout;
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

            //GoogleMobileAd.OnInterstitialLoaded += OnInterstisialsLoaded;
            GoogleMobileAd.OnInterstitialOpened += OnInterstisialsOpen;
            GoogleMobileAd.OnInterstitialClosed += OnInterstisialsClosed;

            //GoogleMobileAd.OnRewardedVideoLoaded += HandleOnRewardedVideoLoaded;
            GoogleMobileAd.OnRewardedVideoAdClosed += HandleOnRewardedVideoAdClosed;
			GoogleMobileAd.OnRewardedVideoAdOpened += HandleOnRewardedVideoAdOpened;
			//GoogleMobileAd.OnRewardedVideoStarted += HandleOnRewardedVideoAdStarted;

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
            //GoogleMobileAd.OnRewardedVideoStarted -= HandleOnRewardedVideoAdStarted;

        }

        void HandleOnRewardedVideoLoaded()
        {
            Debug.Log("Load Video Rewarded");
            GoogleMobileAd.OnRewardedVideoLoaded -= HandleOnRewardedVideoLoaded;
            if (Time.time < _rewardedAdTimeout) {
                FirebaseAnalyticsManager.Instance.SendRewardedAdverStart();
                GoogleMobileAd.ShowRewardedVideo();
                _rewardedAdTimeout = 0;
            }
        }

        void HandleOnRewardedVideoAdOpened()
        {
            Debug.Log("Opened Video Rewarded");
			Time.timeScale = 0;
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
            GoogleMobileAd.LoadRewardedVideo();
			Time.timeScale = 1;
        }

        void OnInterstisialsLoaded()
        {
            GoogleMobileAd.OnInterstitialLoaded -= OnInterstisialsLoaded;
            if (Time.time < _intersticialAdTimeout) {
                FirebaseAnalyticsManager.Instance.SendIntersticialAdverStart();
                GoogleMobileAd.ShowInterstitialAd();
                _intersticialAdTimeout = 0;
            }
            //ad loaded, strting ad
        }

        void OnInterstisialsOpen()
        {
            //pausing the game
			Time.timeScale = 0;
        }

        void OnInterstisialsClosed()
        {
            //un-pausing the game
            GoogleMobileAd.LoadInterstitialAd();
			Time.timeScale = 1;
        }

        public void ShowRewardedVideoAd()
        {
            if (GoogleMobileAd.IsRewardedVideoReady) {
                FirebaseAnalyticsManager.Instance.SendRewardedAdverStart();
                GoogleMobileAd.ShowRewardedVideo();
            } else {
                GoogleMobileAd.OnRewardedVideoLoaded += HandleOnRewardedVideoLoaded;
                GoogleMobileAd.LoadRewardedVideo();
                _rewardedAdTimeout = Time.time + _adLoadTimeout;
                
            }
            //GoogleMobileAd.LoadRewardedVideo();
        }

        public void ShowInterstisialAd()
        {
            if (GoogleMobileAd.IsInterstitialReady) {
                FirebaseAnalyticsManager.Instance.SendIntersticialAdverStart();
                GoogleMobileAd.ShowInterstitialAd();
            } else {
                GoogleMobileAd.OnInterstitialLoaded += OnInterstisialsLoaded;
                GoogleMobileAd.LoadInterstitialAd();
                _intersticialAdTimeout = Time.time + _adLoadTimeout;
            }
            //GoogleMobileAd.LoadInterstitialAd();
            _nextTimeAds = Time.time + _timeAds;
        }
    }
}

