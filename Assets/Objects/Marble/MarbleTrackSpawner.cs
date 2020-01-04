using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleTrackSpawner : MonoBehaviour
{
    [SerializeField] private SpawnPointsHolder spawnPointsHolder = null;
    [SerializeField] private EntrancePointsHolder entrancePointsHolder = null;
    [SerializeField] private GameStateVariable gameState = null;

    [SerializeField] private int angleBetweenTrack = 7;
    private bool[] quadrants;

    [SerializeField] private int numberOfStartingTracks = 2;
    private int numberOfSpawnedTracks = 0;
    [SerializeField] private float spawnOffset = 3;
    [Range(0f,1f)]
    [SerializeField] private float chanceToSpawnAPair = 0.5f;

    private void Start()
    {
        quadrants = new bool[(int)(360/ angleBetweenTrack)];
        StartCoroutine(CreateStartingTracks());
    }

    private IEnumerator CreateStartingTracks()
    {
        while(gameState.value != GameStateVariable.GameState.Running)
        {
            yield return new WaitForSeconds(0.5f);
        }

        for (int i = 0; i < numberOfStartingTracks; i++)
        {
            int angle = 0; // Random.Range(0, 360);
            int quadrentIndex = Mathf.FloorToInt(angle / angleBetweenTrack);
            if (numberOfSpawnedTracks < quadrants.Length)
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
            if (numberOfSpawnedTracks < quadrants.Length)
            {
                CreateRandomTrack();
            }
        }
        else if(len > 1)
        {
            //Chance to make a paired entrance
            float chance = Random.value;
            if (chance < chanceToSpawnAPair)
            {
                // Choose a random track
                SpawnPoint point = spawnPointsHolder.ListOfSpawnPoints[Random.Range(0, spawnPointsHolder.ListOfSpawnPoints.Count)];

                if (numberOfSpawnedTracks < quadrants.Length)
                {
                    int angle = entrancePointsHolder.ListOfEntrancePoints[Random.Range(0, entrancePointsHolder.ListOfEntrancePoints.Count)].angleFromRight + point.angleFromRight;
                    angle = (int)PlayerController.SimplifyAngle(angle);
                    int quadrentIndex = Mathf.FloorToInt(angle / angleBetweenTrack);
                    while (quadrants[quadrentIndex])
                    {
                        angle = entrancePointsHolder.ListOfEntrancePoints[Random.Range(0, entrancePointsHolder.ListOfEntrancePoints.Count)].angleFromRight + point.angleFromRight;
                        angle = (int)PlayerController.SimplifyAngle(angle);
                        quadrentIndex = Mathf.FloorToInt(angle / angleBetweenTrack);
                    }
                    CreateNewTrack(quadrentIndex * angleBetweenTrack);// quadrentIndex * angleBetweenTrack);
                    quadrants[quadrentIndex] = true;
                }
            }
            else
            {
                if (numberOfSpawnedTracks < quadrants.Length)
                {
                    CreateRandomTrack();
                }
            }
        }
    }

    private void CreateRandomTrack()
    {
        int angle = Random.Range(0, 360);
        int quadrentIndex = Mathf.FloorToInt(angle / angleBetweenTrack);
        while (quadrants[quadrentIndex])
        {
            angle = Random.Range(0, 360);
            quadrentIndex = Mathf.FloorToInt(angle / angleBetweenTrack);
        }
        CreateNewTrack(quadrentIndex * angleBetweenTrack);
        quadrants[quadrentIndex] = true;
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
