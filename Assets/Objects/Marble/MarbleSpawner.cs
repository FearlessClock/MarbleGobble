using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleSpawner : MonoBehaviour
{
    [SerializeField] private MarbleController marblePrefab = null;
    [SerializeField] private SpawnPointsHolder spawnPointsHolder = null;
    [SerializeField] private EntrancePointsHolder entrancePointsHolder = null;
    [SerializeField] private float timeTillNextSpawn = 1f;
    private float timer = 0;
    [SerializeField] private float spawnRadius = 1;
    private float diffImprover = 0;

    SpawnLocationCalculator spawnLocationCalculator = null;

    private void Awake()
    {
        spawnLocationCalculator = new SpawnLocationCalculator();

        spawnPointsHolder.OnValueChanged += UpdateSpawnMatrix;
        entrancePointsHolder.OnValueChanged += UpdateEntranceMatrix;
        marblePrefab.gameObject.SetActive(false);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            diffImprover += 0.1f;
            if(diffImprover > timeTillNextSpawn + 0.5f)
            {
                diffImprover = timeTillNextSpawn - 0.5f;
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
