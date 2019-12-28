using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleSpawner : MonoBehaviour
{
    [SerializeField] private MarbleController marblePrefab = null;
    [SerializeField] private SpawnPointsHolder spawnPointsHolder = null;
    [SerializeField] private float timeTillNextSpawn = 1f;
    private float timer = 0;
    [SerializeField] private float spawnRadius = 1;
    private float diffImprover = 0;
    private void Awake()
    {
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
            timer = timeTillNextSpawn - diffImprover;
            MarbleController marble = Instantiate<MarbleController>(marblePrefab, this.transform);
            marble.Target = Vector2.zero;
            marble.StartingTarget = spawnPointsHolder.ListOfSpawnPoints[Random.Range(0, spawnPointsHolder.ListOfSpawnPoints.Count)].spawnPoint;
            marble.gameObject.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, spawnRadius);
    }
}
