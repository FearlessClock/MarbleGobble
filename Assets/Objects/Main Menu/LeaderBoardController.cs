using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardController : MonoBehaviour
{
    public void ShowLeaderBoard()
    {
        if(GameServices.LocalUser != null)
        {
            GameServices.ShowLeaderboardUI(GPGSIds.leaderboard_highscore);
        }
        else
        {
            GameServices.Init();
        }
    }
}
