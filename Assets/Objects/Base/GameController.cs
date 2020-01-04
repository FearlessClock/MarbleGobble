using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

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

    [SerializeField] private BoolVariable hasRevived = null;

    [Header("Countdown Variables")]
    [SerializeField] private float countdownAmount = 3;
    private float countdownTimer = 0;
    [SerializeField] private TextMeshProUGUI countdownTimerText = null;
    [SerializeField] private Animator countdownTimerAnimator = null;

    private void Awake()
    {
        trackToPipeRatio += Random.Range(-1, 2);
        ResetCountdownTimer();
        hasRevived.SetValue(false);
        gameState.SetValue(GameStateVariable.GameState.Countdown);
        gameState.OnValueChanged.AddListener(StateUpdate);
    }

    private void ResetCountdownTimer()
    {
        countdownTimer = countdownAmount;
        countdownTimerText.gameObject.SetActive(true);
        countdownTimerText.SetText(((int)countdownTimer).ToString());
    }

    private void StateUpdate()
    {
        switch (gameState.value)
        {
            case GameStateVariable.GameState.MainMenu:
                break;
            case GameStateVariable.GameState.Countdown:
                ResetCountdownTimer();
                break;
            case GameStateVariable.GameState.Running:
                break;
            case GameStateVariable.GameState.Pause:
                break;
            case GameStateVariable.GameState.GameOver:
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if(gameState.value == GameStateVariable.GameState.Running)
        {
            if ((score.value - lastScore) >= scoreStep)
            {
                lastScore = score.value;
                if (spawnPointsHolder.ListOfSpawnPoints.Count % trackToPipeRatio == 0)
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
        else if(gameState.value == GameStateVariable.GameState.Countdown)
        {
            countdownTimer -= Time.deltaTime;
            // Only update the text when the amount in ints changes and run an animation
            if (!((int)countdownTimer + 1).ToString().Equals(countdownTimerText.text))
            {
                countdownTimerText.SetText(((int)countdownTimer+1).ToString());
                countdownTimerAnimator.SetTrigger("Update");
            }
            if(countdownTimer <= 0)
            {
                gameState.SetValue(GameStateVariable.GameState.Running);
                countdownTimer = countdownAmount;
                countdownTimerText.gameObject.SetActive(false);
            }
        }
    }

    public void TakenHit()
    {
        // TODO : Only take 1 damage per wave of marbles
        numberOfLives -= 1;
        if(numberOfLives <= 0)
        {
            OnDied?.Invoke();
            gameState.SetValue(GameStateVariable.GameState.GameOver);
        }
        lifeManager.UpdateLives(numberOfLives);
    }

    public void Revive()
    {
        numberOfLives = 3;
        gameState.SetValue(GameStateVariable.GameState.Countdown);
        lifeManager.UpdateLives(numberOfLives);
    }
}
