using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperScripts.EventSystem;
using EasyMobile;
using UnityEngine.Events;

public class ReviveController : MonoBehaviour
{
    [SerializeField] private EventObjectScriptable OnRevive = null;
    [SerializeField] private BoolVariable hasRevived = null;
    [SerializeField] private GameStateVariable gameState = null;
    [SerializeField] private GameStateVariable.GameState continueState = GameStateVariable.GameState.Running;

    public UnityEvent OnRewardedAdCompleted;
    public UnityEvent OnRewardedAdSkip;
    public UnityEvent OnHasSeenAd;
    private void Awake()
    {
        if (hasRevived)
        {
            OnHasSeenAd?.Invoke();
            return;
        }
    }
    private void OnEnable()
    {
        Advertising.RewardedAdCompleted += Advertising_RewardedAdCompleted;
        Advertising.RewardedAdSkipped += Advertising_RewardedAdSkipped;
    }

    private void OnDisable()
    {
        Advertising.RewardedAdCompleted -= Advertising_RewardedAdCompleted;
        Advertising.RewardedAdSkipped -= Advertising_RewardedAdSkipped;
    }

    private void Advertising_RewardedAdSkipped(RewardedAdNetwork arg1, AdPlacement arg2)
    {
        OnRewardedAdSkip?.Invoke();
    }

    private void Advertising_RewardedAdCompleted(RewardedAdNetwork arg1, AdPlacement arg2)
    {
        gameState.SetValue(continueState);
        OnRewardedAdCompleted?.Invoke();
        OnRevive.Call(null);
    }

    public void Revive()
    {
#if UNITY_EDITOR 
        hasRevived.SetValue(true);
        Advertising_RewardedAdCompleted(RewardedAdNetwork.UnityAds, null);
        return;
#else
        if (Advertising.IsRewardedAdReady())
        {
            Debug.Log("Ad is ready, Loading ad");
            Advertising.ShowRewardedAd();
            hasRevived.SetValue(true);
        }
        else
        {
            Debug.Log("Rewarded ad not ready");
        }
#endif
    }
}
