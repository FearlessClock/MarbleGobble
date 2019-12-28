using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnPoint
{
    public int ID;
    public Vector2 spawnPoint;
    public Vector2 middlePoint;
    public int angleFromRight;
}

[CreateAssetMenu(fileName = "New Spawn Point Holder", menuName = "MarbleGobble/Spawn Point Holder")]
public class SpawnPointsHolder : ScriptableObject
{
    public List<SpawnPoint> listOfSpawnPoints = new List<SpawnPoint>();
    public Action OnValueChanged;
    private static int numberOfTracks = 0;
    
    public List<SpawnPoint> ListOfSpawnPoints
    {
        get { return listOfSpawnPoints; }
        set { listOfSpawnPoints = value; OnValueChanged?.Invoke(); }
    }

    public void CreateNewTrack(int angle, float offset)
    {
        Vector2 pos = new Vector2(offset, 0);
        pos = Quaternion.Euler(0, 0, angle) * pos;
        SpawnPoint spawnPoint = new SpawnPoint()
        {
            ID = numberOfTracks++,
            angleFromRight = angle,
            middlePoint = pos,
            spawnPoint = pos * 2
        };
        listOfSpawnPoints.Add(spawnPoint);
        OnValueChanged?.Invoke();
    }

    public SpawnPoint GetSpawnPoint(int ID)
    {
        foreach (SpawnPoint spawnPoint in listOfSpawnPoints)
        {
            if (spawnPoint.ID == ID)
            {
                return spawnPoint;
            }
        }

        Debug.Log("Spawn point with ID:" + ID + " wasn't found in list");

        return new SpawnPoint
        {
            ID = -1,
            spawnPoint = Vector2.zero,
            angleFromRight = 0
        };
    }
}
