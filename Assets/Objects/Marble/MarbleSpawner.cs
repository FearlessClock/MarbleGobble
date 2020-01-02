using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleSpawner : MonoBehaviour
{
    [SerializeField] private GameStateVariable gamestate = null;
    [SerializeField] private float diffStep = 0.1f;
    [SerializeField] private float diffLimit = 1f;
    [SerializeField] private MarbleController marblePrefab = null;
    [SerializeField] private SpawnPointsHolder spawnPointsHolder = null;
    [SerializeField] private EntrancePointsHolder entrancePointsHolder = null;
    [SerializeField] private float timeTillNextSpawn = 1f;
    private float timer = 0;
    private float spawnRadius = 1;
    [SerializeField] private float offSetMultiplier = 0.9f;
    private float diffImprover = 0;

    SpawnLocationCalculator spawnLocationCalculator = null;

    private void Awake()
    {
        spawnLocationCalculator = new SpawnLocationCalculator();

        spawnPointsHolder.OnValueChanged += UpdateSpawnMatrix;
        entrancePointsHolder.OnValueChanged += UpdateEntranceMatrix;
        marblePrefab.gameObject.SetActive(false);

        spawnRadius = Camera.main.orthographicSize * Camera.main.aspect * offSetMultiplier;
    }

    private void Update()
    {
        if (gamestate.value != GameStateVariable.GameState.Running)
        {
            return;
        }
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            diffImprover += diffStep;
            if(diffImprover > timeTillNextSpawn + diffLimit)
            {
                diffImprover = timeTillNextSpawn - diffLimit;
            }

            List<int> spawnPositionsAsIDs = spawnLocationCalculator.GetRandomSpawns();
            if(spawnPositionsAsIDs != null)
            {
                foreach (int pos in spawnPositionsAsIDs)
                {
                    MarbleController marble = Instantiate<MarbleController>(marblePrefab, this.transform);
                    marble.Target = Vector2.zero;
                    marble.StartingTarget = spawnPointsHolder.ListOfSpawnPoints[pos].spawnPoint;
                    marble.gameObject.SetActive(true);
                }
            }

            timer = timeTillNextSpawn - diffImprover;
        }
    }
    
    public void UpdateSpawnMatrix()
    {
        if(spawnPointsHolder.ListOfSpawnPoints.Count > 1)
        {
            spawnLocationCalculator.UpdateSpawnMatrix(spawnPointsHolder);
            spawnLocationCalculator.UpdateSpawnLocationsByNbrOfBalls();
            spawnLocationCalculator.PrintBuckets();
        }
    }

    public void UpdateEntranceMatrix()
    {
        if(entrancePointsHolder.ListOfEntrancePoints.Count > 1)
        {
            spawnLocationCalculator.UpdateEntranceMatrix(entrancePointsHolder);
            spawnLocationCalculator.UpdateSpawnLocationsByNbrOfBalls();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, spawnRadius);
    }
}
