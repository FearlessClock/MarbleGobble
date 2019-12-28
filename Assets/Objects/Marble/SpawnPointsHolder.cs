using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct SpawnPoint
{
    public int ID;
    public Vector2 spawnpoint;
    public Vector2 middlePoint;
    public float angleFromRight;
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

    public void CreateNewTrack(float angle, float offset)
    {
        Vector2 pos = new Vector2(offset, 0);
        pos = Quaternion.Euler(0, 0, angle) * pos;
        SpawnPoint spawnPoint = new SpawnPoint()
        {
            ID = numberOfTracks++,
            angleFromRight = angle,
            middlePoint = pos,
            spawnpoint = pos * 2
        };
        listOfSpawnPoints.Add(spawnPoint);
        OnValueChanged?.Invoke();
    }
}
