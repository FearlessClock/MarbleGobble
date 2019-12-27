using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New PlayerPrefIntVariable", menuName = "UnityHelperScripts/PlayerPrefs/PlayerPrefIntVariable", order = 0)]
public class PlayerPrefIntVariable : ScriptableObject
{
    public string ID;
    public int value;

    public UnityEngine.Events.UnityEvent OnValueChanged;

    private void OnEnable()
    {
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetInt(ID, value);
    }

    public void Load()
    {
        value = PlayerPrefs.GetInt(ID, 0);
    }
    public void SetValue(int value)
    {
        this.value = value;
        Save();
        OnValueChanged?.Invoke();
    }

    public int GetLatestValue()
    {
        Load();
        return value;
    }
}
