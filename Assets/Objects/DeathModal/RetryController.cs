using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RetryController : MonoBehaviour
{
    [SerializeField] private FloatVariable numberOfReloads = null;
    [SerializeField] private int nmbrOfRestartsBeforeShowingAd = 3;
    [SerializeField] private PlayerPrefIntVariable HasRemovedAds = null;
    public UnityEvent OnReloadLevel;
    private void OnEnable()
    {
        Advertising.InterstitialAdCompleted += Advertising_InterstitialAdCompleted;
    }

    private void OnDisable()
    {
        Advertising.InterstitialAdCompleted -= Advertising_InterstitialAdCompleted;
    }

    private void Advertising_InterstitialAdCompleted(InterstitialAdNetwork arg1, AdPlacement arg2)
    {
        OnReloadLevel?.Invoke();
    }

    public void ReloadLevel()
    {
        numberOfReloads.Add(1);
        if (HasRemovedAds.GetLatestValue()  == 0 && numberOfReloads > nmbrOfRestartsBeforeShowingAd && Advertising.IsInterstitialAdReady())
        {
            Advertising.ShowInterstitialAd(AdPlacement.GameOver);
            numberOfReloads.SetValue(0);
        }
        else
        {
            OnReloadLevel?.Invoke();
        }
    }

}
