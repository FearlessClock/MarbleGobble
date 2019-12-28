using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnPoint
{
    public int ID;
    public Vector2 spawnPoint;
    public int angleFromRight;
}

[CreateAssetMenu(fileName = "New Spawn Point Holder", menuName = "MarbleGobble/Spawn Point Holder")]
public class SpawnPointsHolder : ScriptableObject
{
    public List<SpawnPoint> listOfSpawnPoints = null;
    public Action OnValueChanged; 
    
    public List<SpawnPoint> ListOfSpawnPoints
    {
        get { return listOfSpawnPoints; }
        set { listOfSpawnPoints = value; OnValueChanged?.Invoke(); }
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
