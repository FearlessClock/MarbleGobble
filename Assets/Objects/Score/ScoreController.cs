using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private PlayerPrefIntVariable playerScore = null;
    private void Awake()
    {
        playerScore.SetValue(0);
    }
    public void AddToScore(int value)
    {
        playerScore.SetValue(playerScore.GetLatestValue() + value);
    }
}
