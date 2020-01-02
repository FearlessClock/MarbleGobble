using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private SpawnPointsHolder spawnPointsHolder = null;
    [SerializeField] private EntrancePointsHolder entrancePointsHolder = null;

    [SerializeField] private MarbleTrackSpawner marbleTrackSpawner = null;
    [SerializeField] private PlayerGenerator playerGenerator = null;

    [Tooltip("How many tracks for each pipe")]
    [SerializeField] private int trackToPipeRatio = 5;

    [SerializeField] private PlayerPrefIntVariable score = null;
    [SerializeField] private int scoreStep = 100;
    private int lastScore = 0;

    private void Update()
    {
        if((score.value - lastScore) > scoreStep )
        {
            lastScore = score.value;
            if(spawnPointsHolder.ListOfSpawnPoints.Count % 5 == 0)
            {
                playerGenerator.AddRandomBranch();
                marbleTrackSpawner.AddNewTrack();
            }
            else
            {
                marbleTrackSpawner.AddNewTrack();
            }
        }
    }
}
