using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleTrackSpawner : MonoBehaviour
{
    [SerializeField] private SpawnPointsHolder spawnPointsHolder = null;
    [SerializeField] private EntrancePointsHolder entrancePointsHolder = null;

    [SerializeField] private int angleBetweenTrack = 7;
    private bool[] quadrants;

    [SerializeField] private int numberOfStartingTracks = 2;
    private int numberOfSpawnedTracks = 0;
    [SerializeField] private float spawnOffset = 3;

    private void Start()
    {
        quadrants = new bool[(int)(360/ angleBetweenTrack)];
        for (int i = 0; i < numberOfStartingTracks; i++)
        {
            int angle = Random.Range(0, 360);
            int quadrentIndex = Mathf.FloorToInt(angle / angleBetweenTrack);
            if(numberOfSpawnedTracks < quadrants.Length)
            {
                while (quadrants[quadrentIndex])
                {
                    angle = Random.Range(0, 360);
                    quadrentIndex = Mathf.FloorToInt(angle / angleBetweenTrack);
                }
                CreateNewTrack(quadrentIndex * angleBetweenTrack);
                quadrants[quadrentIndex] = true;
            }
        }
    }

    private void CreateNewTrack(int angle)
    {
        spawnPointsHolder.CreateNewTrack(angle, spawnOffset);
        numberOfSpawnedTracks++;
    }

    public void AddNewTrack()
    {
        int len = entrancePointsHolder.ListOfEntrancePoints.Count;
        if(len == 1)
        {
            CreateNewTrack(Random.Range(0, 360));
        }
        else if(len > 1)
        {

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
