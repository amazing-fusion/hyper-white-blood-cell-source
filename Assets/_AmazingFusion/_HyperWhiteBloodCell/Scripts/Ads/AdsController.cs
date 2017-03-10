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
        float _adLoadTimeout;

        float _nextTimeAds;

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

        void Start()
        {
            _nextTimeAds = Time.time + (_timeAds * 60);

            GoogleMobileAd.Init();

            //GoogleMobileAd.OnInterstitialLoaded += OnInterstisialsLoaded;
            GoogleMobileAd.OnInterstitialOpened += OnInterstisialsOpen;
            GoogleMobileAd.OnInterstitialClosed += OnInterstisialsClosed;

            //GoogleMobileAd.OnRewardedVideoLoaded += HandleOnRewardedVideoLoaded;
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
            GoogleMobileAd.OnRewardedVideoLoaded -= HandleOnRewardedVideoLoaded;
            if (Time.time < _rewardedAdTimeout) {
                GoogleMobileAd.ShowRewardedVideo();
                _rewardedAdTimeout = 0;
            }
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
            GoogleMobileAd.LoadRewardedVideo();
        }

        void OnInterstisialsLoaded()
        {
            GoogleMobileAd.OnInterstitialLoaded -= OnInterstisialsLoaded;
            if (Time.time < _intersticialAdTimeout) {
                GoogleMobileAd.ShowInterstitialAd();
                _intersticialAdTimeout = 0;
            }
            //ad loaded, strting ad
        }

        void OnInterstisialsOpen()
        {
            //pausing the game
            
        }

        void OnInterstisialsClosed()
        {
            //un-pausing the game
            GoogleMobileAd.LoadInterstitialAd();
        }

        public void ShowRewardedVideoAd()
        {
            if (GoogleMobileAd.IsRewardedVideoReady) {
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

