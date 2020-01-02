using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField] private int numberOfLives = 3;
    [SerializeField] private GameStateVariable gameState = null;
    [SerializeField] private LifeManager lifeManager = null;
    public UnityEvent OnDied;

    private void Awake()
    {
        gameState.SetValue(GameStateVariable.GameState.Running);
    }

    private void Update()
    {
        if(gameState.value == GameStateVariable.GameState.Running)
        {
            if ((score.value - lastScore) > scoreStep)
            {
                lastScore = score.value;
                if (spawnPointsHolder.ListOfSpawnPoints.Count % 5 == 0)
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

    public void TakenHit()
    {
        numberOfLives -= 1;
        if(numberOfLives <= 0)
        {
            OnDied?.Invoke();
            gameState.SetValue(GameStateVariable.GameState.GameOver);
        }
        lifeManager.UpdateLives(numberOfLives);
    }
}
