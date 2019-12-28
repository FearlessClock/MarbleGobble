using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleTrackSpawner : MonoBehaviour
{
    [SerializeField] private SpawnPointsHolder spawnPointsHolder = null;

    [SerializeField] private int numberOfStartingTracks = 4;
    [SerializeField] private int numberOfSpawnedTracks = 0;
    [SerializeField] private float spawnOffset = 3;

    private void Start()
    {
        for (int i = 0; i < numberOfStartingTracks; i++)
        {
            spawnPointsHolder.CreateNewTrack(Random.Range(0, 360), spawnOffset);
            numberOfSpawnedTracks++;
        }
    }
    private void OnDestroy()
    {
        spawnPointsHolder.ListOfSpawnPoints.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, spawnOffset);
    }

}
