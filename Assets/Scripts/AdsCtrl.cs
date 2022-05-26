using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsCtrl : MonoBehaviour
{
    public static AdsCtrl instance = null;
    public string Android_Admob_Banner_ID;     // test id
    public string Android_Admob_Interstitial_ID; // test id

    BannerView bannerView;   // the container for the banner ad
    public bool testMode;  // to enable/disable test ads
    public bool showBanner;  // to toggle banner ad
    public bool showInterstitial; // to toggle ad
    InterstitialAd interstitial; // container
    AdRequest request;  // requests ad


    // live ids
    public string Admob_Banner_Live;
    public string Admob_Interstitial_Live;

    // Start is called before the first frame update
    void Start()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

    }

    public void RequestBanner()
    {
        if (testMode)
        {
            // create a 320x50 banner at the top of the screen
            bannerView = new BannerView(Android_Admob_Banner_ID, AdSize.Banner, AdPosition.Top);
        }
        else
        {
            // real ads code
            bannerView = new BannerView(Admob_Banner_Live, AdSize.Banner, AdPosition.Top);
        }

        // create an empty ad
        AdRequest adRequest = new AdRequest.Builder().Build();

        // load the banner with the request
        bannerView.LoadAd(adRequest);

        HideBanner(); // to hide banner on main screen of game
    }

    public void ShowBanner()
    {
        if (showBanner)
        {
            bannerView.Show();
        }
    }

    public void HideBanner()
    {
        if (showBanner)
        {
            bannerView.Hide();
        }
    }

    public void HideBanner(float duration)
    {
        StartCoroutine("HideBannerRoutine", duration);
    }

    IEnumerator HideBannerRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        bannerView.Hide();
    }

    // requests ad
    void RequestInterstitial()
    {
        // Initialize interstitial ad
        if (testMode)
        {
            interstitial = new InterstitialAd(Android_Admob_Interstitial_ID);
        }
        else
        {
            // code forlive id
            interstitial = new InterstitialAd(Admob_Interstitial_Live);
        }

        // create an empty adrequest
        request = new AdRequest.Builder().Build();

        // Load the INterstitial with the request
        interstitial.LoadAd(request);

        interstitial.OnAdClosed += HandleOnAdClosed;
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        interstitial.Destroy();
        RequestInterstitial();
    }

    public void ShowINterstitial()
    {
        if (showInterstitial)
        {
            if (interstitial.IsLoaded())
            {
                interstitial.Show();
            }
        }
    }

    private void OnEnable()
    {
        if (showBanner)
        {
            RequestBanner();
        }

        if (showInterstitial)
        {
            RequestInterstitial();
        }
    }
    private void OnDisable()
    {
        if (showBanner)
        {
            bannerView.Destroy();
        }

        if (showInterstitial)
        {
            interstitial.Destroy();
        }
    }
}
