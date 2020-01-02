using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBannerAd : MonoBehaviour
{
    void Start()
    {
        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
    }

    private void OnDisable()
    {
        Advertising.DestroyBannerAd();
    }
}
