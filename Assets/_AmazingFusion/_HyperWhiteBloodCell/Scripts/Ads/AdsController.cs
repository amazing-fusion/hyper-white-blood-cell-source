using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class AdsController : Singleton<AdsController>
    {
        void Start()
        {
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
        }

        void HandleOnRewardedVideoLoaded()
        {

        }

        void HandleOnRewardedVideoAdClosed()
        {

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
        }
    }
}

