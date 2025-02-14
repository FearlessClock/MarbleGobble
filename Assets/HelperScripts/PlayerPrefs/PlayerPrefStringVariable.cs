﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerPrefStringVariable", menuName = "UnityHelperScripts/PlayerPrefs/PlayerPrefStringVariable", order = 0)]
public class PlayerPrefStringVariable : ScriptableObject
{
    public string ID;
    public string value;

    private void OnEnable()
    {
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetString(ID, value);
    }

    public void Load()
    {
        value = PlayerPrefs.GetString(ID);
    }
    public void SetValue(string value)
    {
        this.value = value;
        Save();
    }

    public string GetLatestValue()
    {
        Load();
        return value;
    }
}
