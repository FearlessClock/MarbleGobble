using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreController : MonoBehaviour
{
    [SerializeField] private PlayerPrefIntVariable localHighscore = null;
    [SerializeField] private PlayerPrefIntVariable score = null;

    public void Awake()
    {
        if(score.GetLatestValue() > localHighscore.GetLatestValue())
        {
            localHighscore.SetValue(score.value);
            GameServices.ReportScore(score.value, GPGSIds.leaderboard_highscore);
        }
    }
}
