using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Entrance
{
    int id;
    float refAngle;
}

public class SpawnLocationCalculator
{
    private int[,] angleBTWSpawnPoints = { { } };
    private int[,] angleBTWEntrances = { { } };

    public int[][] spawnLocationsByNbrOfBalls = null;

    public void UpdateMatrices(SpawnPointsHolder spawnPointsHolder, EntrancePointsHolder entrancePointsHolder)
    {
        // Spawn points
        int nbrSpawnPoints = spawnPointsHolder.ListOfSpawnPoints.Count;

        angleBTWSpawnPoints = new int[nbrSpawnPoints, nbrSpawnPoints];

        for (int i = 0; i < nbrSpawnPoints; i++)
        {
            for (int j = 0; j < nbrSpawnPoints; j++)
            {
                if(i == j)
                {
                    angleBTWSpawnPoints[i, j] = 0;
                } else
                {
                    angleBTWSpawnPoints[i, j] = -spawnPointsHolder.ListOfSpawnPoints[i].angleFromRight + spawnPointsHolder.ListOfSpawnPoints[j].angleFromRight;
                }
            }
        }
        
        // Entrance points
        int nbrEntrancePoints = entrancePointsHolder.ListOfEntrancePoints.Count;

        angleBTWEntrances = new int[nbrEntrancePoints, nbrEntrancePoints];

        for (int i = 0; i < nbrEntrancePoints; i++)
        {
            for (int j = 0; j < nbrEntrancePoints; j++)
            {
                if (i == j)
                {
                    angleBTWEntrances[i, j] = 0;
                }
                else
                {
                    angleBTWEntrances[i, j] = -entrancePointsHolder.ListOfEntrancePoints[i].angleFromRight + entrancePointsHolder.ListOfEntrancePoints[j].angleFromRight;
                }
            }
        }
    }

    public void updateSpawnLocationsByNbrOfBalls(int maxNbrToSpawn)
    {

    }

    public void PrintMatrices()
    {
        int nbrSpawnPoints = angleBTWSpawnPoints.Length;

        Debug.Log("\nSpawnPoint matrix:\n");
        for (int i = 0; i < nbrSpawnPoints; i++)
        {
            for (int j = 0; j < nbrSpawnPoints; j++)
            {
                Debug.Log(angleBTWSpawnPoints[i, j] + " ");
            }
            Debug.Log("\n\n");
        }

        int nbrEntrancePoints = angleBTWEntrances.Length;

        Debug.Log("Entrance points matrix:\n");
        for (int i = 0; i < nbrEntrancePoints; i++)
        {
            for (int j = 0; j < nbrEntrancePoints; j++)
            {
                Debug.Log(angleBTWEntrances[i,j] + " ");
            }
            Debug.Log("\n\n");
        }
    }
}


