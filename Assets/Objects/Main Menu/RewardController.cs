using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static IAPController;

public class RewardController : MonoBehaviour
{
    [SerializeField] private PlayerPrefIntVariable currentCoins = null;
    [SerializeField] private IAPController iAPController = null;

    public FloatEvent OnCoinsChange;
    
    private void Awake()
    {
        currentCoins.OnValueChanged.AddListener(OnChangeCoins);
    }

    private void OnChangeCoins()
    {
        OnCoinsChange?.Invoke(currentCoins.GetLatestValue());
    }

    public void GainCoins(int amount)
    {
        ChangeCoins(amount);
    }

    public bool LoseCoins(int amount)
    {
        if(currentCoins.GetLatestValue() >= amount)
        {
            ChangeCoins(-amount);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanBuy(int amount)
    {
        return currentCoins.GetLatestValue() >= amount;
    }

    private void ChangeCoins(int amount)
    {
        currentCoins.SetValue(currentCoins.GetLatestValue() + amount);
    }

    /// <summary>
    /// Used when buying packs of coins
    /// </summary>
    /// <param name="amount"></param>
    public void CoinsBought(int amount)
    {
        GainCoins(amount);
    }
}
