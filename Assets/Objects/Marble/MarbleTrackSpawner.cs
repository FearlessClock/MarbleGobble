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
            int angle = 0; // Random.Range(0, 360);
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
        Invoke("AddNewTrack", 6);
        Invoke("AddNewTrack", 11);
        Invoke("AddNewTrack", 16);
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
            if (numberOfSpawnedTracks < quadrants.Length)
            {
                CreateNewTrack(Random.Range(0, 360));
            }
        }
        else if(len > 1)
        {
            //Chance to make a paired entrance
            if (true)
            {
                // Choose a random track
                SpawnPoint point = spawnPointsHolder.ListOfSpawnPoints[Random.Range(0, spawnPointsHolder.ListOfSpawnPoints.Count)];

                if (numberOfSpawnedTracks < quadrants.Length)
                {
                    int angle = entrancePointsHolder.ListOfEntrancePoints[Random.Range(0, entrancePointsHolder.ListOfEntrancePoints.Count)].angleFromRight + point.angleFromRight;
                    angle = (int)PlayerController.SimplifyAngle(angle);
                    int quadrentIndex = Mathf.FloorToInt(angle / angleBetweenTrack);
                    if(!quadrants[quadrentIndex])
                    {
                        CreateNewTrack(angle);// quadrentIndex * angleBetweenTrack);
                        quadrants[quadrentIndex] = true;
                    }
                    string info = "";
                    foreach (SpawnPoint item in spawnPointsHolder.ListOfSpawnPoints)
                    {
                        info += item.ID + " - " + item.angleFromRight + " : ";
                    }
                    foreach (EntrancePoint item in entrancePointsHolder.ListOfEntrancePoints)
                    {
                        info += item.ID + " - " + item.angleFromRight + " : ";
                    }
                    Debug.Log(info);
                }
            }
            else
            {
                CreateNewTrack(Random.Range(0, 360));
            }
            // Choose a random Branch

            EntrancePoint entrancePoint = entrancePointsHolder.ListOfEntrancePoints[Random.Range(0, entrancePointsHolder.ListOfEntrancePoints.Count)];

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
