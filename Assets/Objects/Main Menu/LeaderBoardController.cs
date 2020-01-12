using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardController : MonoBehaviour
{
    public void ShowLeaderBoard()
    {
        if (GameServices.IsInitialized())
        {
            GameServices.ShowLeaderboardUI("Highscore");
        }
        else
        {
            GameServices.Init();
        }
    }
}
