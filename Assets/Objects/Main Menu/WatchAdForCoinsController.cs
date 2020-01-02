using EasyMobile;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WatchAdForCoinsController : MonoBehaviour
{
    [SerializeField] private RewardController rewardController = null;
    [SerializeField] private GameObject adNotReadyGroup = null;
    [SerializeField] private GameObject watchAdObject = null;
    public UnityEvent OnRewardedAdCompleted;
    public UnityEvent OnRewardedAdSkip;
    [SerializeField] private int nmbrOfCoinsWon = 20;

    private void Awake()
    {
        CheckIfAdIsReady();
    }

    private void OnEnable()
    {
        Advertising.RewardedAdCompleted += Advertising_RewardedAdCompleted;
        Advertising.RewardedAdSkipped += Advertising_RewardedAdSkipped;
    }

    private void Advertising_RewardedAdSkipped(RewardedAdNetwork arg1, AdPlacement arg2)
    {
        OnRewardedAdSkip?.Invoke();
    }

    private void Advertising_RewardedAdCompleted(RewardedAdNetwork arg1, AdPlacement arg2)
    {
        rewardController.GainCoins(nmbrOfCoinsWon);
        OnRewardedAdCompleted?.Invoke();
    }

    private void CheckIfAdIsReady()
    {
        if (!Advertising.IsRewardedAdReady())
        {
            watchAdObject.SetActive(false);
        }
    }

    public void ShowAd()
    {
        if (Advertising.IsRewardedAdReady())
        {
            Advertising.ShowRewardedAd();
        }
        else
        {
            OnRewardedAdSkip?.Invoke();
        }
    }
}
