using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monetizer : MonoBehaviour
{
    public bool timedBanner;  // helps in showing ads for certain duration
    public float bannerDuration;  // the duration for which you will show the banner ad

    // Start is called before the first frame update
    void Start()
    {
        AdsCtrl.instance.ShowBanner();
    }

    private void OnDisable()
    {
        if (!timedBanner)
        {
            AdsCtrl.instance.HideBanner();
        }
        else
        {
            AdsCtrl.instance.HideBanner(bannerDuration);
        }

    }
}