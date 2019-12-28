using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public struct Entrance
{
    int id;
    float refAngle;
}

public class SpawnLocationCalculator
{
    private List<List<int>> angleBTWSpawnPoints = new List<List<int>>();
    private List<List<int>> angleBTWEntrances = new List<List<int>>();

    public List<List<List<int>>> spawnLocationsByNbrOfBalls = new List<List<List<int>>>();

    public void UpdateSpawnMatrix(SpawnPointsHolder spawnPointsHolder)
    {
        // Spawn points
        int nbrSpawnPoints = spawnPointsHolder.ListOfSpawnPoints.Count;

        angleBTWSpawnPoints = new List<List<int>>();

        for (int i = 0; i < nbrSpawnPoints; i++)
        {
            angleBTWSpawnPoints.Add(new List<int>());
            for (int j = 0; j < nbrSpawnPoints; j++)
            {
                if(i == j)
                {
                    angleBTWSpawnPoints[i].Add(0);
                } else
                {
                    angleBTWSpawnPoints[i].Add(-spawnPointsHolder.ListOfSpawnPoints[i].angleFromRight + spawnPointsHolder.ListOfSpawnPoints[j].angleFromRight);
                }
            }
        }
    }

    public void UpdateEntranceMatrix(EntrancePointsHolder entrancePointsHolder)
    {
        // Entrance points
        int nbrEntrancePoints = entrancePointsHolder.ListOfEntrancePoints.Count;

        angleBTWEntrances = new List<List<int>>();

        for (int i = 0; i < nbrEntrancePoints; i++)
        {
            angleBTWEntrances.Add(new List<int>());
            for (int j = 0; j < nbrEntrancePoints; j++)
            {
                if (i == j)
                {
                    angleBTWEntrances[i].Add(0);
                }
                else
                {
                    angleBTWEntrances[i].Add(-entrancePointsHolder.ListOfEntrancePoints[i].angleFromRight + entrancePointsHolder.ListOfEntrancePoints[j].angleFromRight);
                }
            }
        }
    }

    public void UpdateSpawnLocationsByNbrOfBalls()
    {
        spawnLocationsByNbrOfBalls = new List<List<List<int>>>();

        for (int i = 0; i < angleBTWSpawnPoints.Count; i++)
        {
            spawnLocationsByNbrOfBalls.Add(new List<List<int>>());
        }

        for (int i = 0; i < angleBTWSpawnPoints.Count; i++)
        {
            List<int> listOfOne = new List<int> { i };
            spawnLocationsByNbrOfBalls[0].Add(listOfOne);
        }

        for (int spawnPnt = 0; spawnPnt < angleBTWSpawnPoints.Count; spawnPnt++)
        {
            for (int entre = 0; entre < angleBTWEntrances.Count; entre++)
            {
                List<int> intersectingValuesAsAngles = GetIntersectingSpawnPoints(angleBTWSpawnPoints[spawnPnt], angleBTWEntrances[entre]);
                List<int> intersectingValuesAsIDs = GetIntersectingSpawnPointsAsIDs(intersectingValuesAsAngles, angleBTWSpawnPoints[spawnPnt]);

                if (intersectingValuesAsIDs.Count > 1)
                {
                    for (int i = 2; i < intersectingValuesAsIDs.Count + 1; i++)
                    {
                        spawnLocationsByNbrOfBalls[i-1].Add(intersectingValuesAsIDs.GetRange(0, i));
                    }
                }
            }
        }

        for (int i = 0; i < spawnLocationsByNbrOfBalls.Count; i++)
        {
            spawnLocationsByNbrOfBalls[i] = spawnLocationsByNbrOfBalls[i].GroupBy(x => string.Join(",", x))
                                 .Select(x => x.First().ToList())
                                 .ToList();
        }
    }

    public List<int> GetIntersectingSpawnPointsAsIDs(List<int> intersectingValuesAsAngles, List<int> spawnPositions)
    {
        List<int> intersectingValuesAsIDs = new List<int>();

        foreach (int val in intersectingValuesAsAngles)
        {
            intersectingValuesAsIDs.Add(spawnPositions.IndexOf(val));
        }

        return intersectingValuesAsIDs;
    }

    public List<int> GetIntersectingSpawnPoints(List<int> spawnPositions, List<int> entrancePositions)
    {
        return spawnPositions.Intersect(entrancePositions).ToList();
    }

    public List<int> GetRandomSpawns()
    {
        int i = Random.Range(0, spawnLocationsByNbrOfBalls.Count);
        while(spawnLocationsByNbrOfBalls[i].Count == 0)
        {
            i = Random.Range(0, spawnLocationsByNbrOfBalls.Count);
        }
            
        int j = Random.Range(0, spawnLocationsByNbrOfBalls[i].Count);

        return spawnLocationsByNbrOfBalls[i][j];
    }

    public void PrintBuckets()
    {
        string output = "";

        for (int i = 0; i < spawnLocationsByNbrOfBalls.Count; i++)
        {
            output += i + " : ";
            for (int j = 0; j < spawnLocationsByNbrOfBalls[i].Count; j++)
            {
                output += "[";
                for (int k = 0; k < spawnLocationsByNbrOfBalls[i][j].Count; k++)
                {
                    output += spawnLocationsByNbrOfBalls[i][j][k] + " ";
                }

                output += "]\t";
            }

            output += "\n";
        }

        Debug.Log(output);
    }

    public void PrintMatrices()
    {
        int nbrSpawnPoints = angleBTWSpawnPoints.Count;
        string output = "SpawnPoint matrix:\n";

        for (int i = 0; i < nbrSpawnPoints; i++)
        {
            for (int j = 0; j < nbrSpawnPoints; j++)
            {
                output += angleBTWSpawnPoints[i][j] + " ";
            }
            output += "\n\n";
        }

        Debug.Log(output);

        int nbrEntrancePoints = angleBTWEntrances.Count;
        output = "Entrance points matrix:\n";

        for (int i = 0; i < nbrEntrancePoints; i++)
        {
            for (int j = 0; j < nbrEntrancePoints; j++)
            {
                output += angleBTWEntrances[i][j] + " ";
            }
            output += "\n\n";
        }
        Debug.Log(output);
    }
}


