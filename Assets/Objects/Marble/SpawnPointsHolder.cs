using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpawnPoint
{
    public int ID;
    public Vector2 spawnpoint;
    public float angleFromRight;
}
[CreateAssetMenu(fileName = "New Spawn Point Holder", menuName = "MarbleGobble/Spawn Point Holder")]
public class SpawnPointsHolder : ScriptableObject
{
    private List<SpawnPoint> listOfSpawnPoints = null;
    public Action OnValueChanged; 
    
    public List<SpawnPoint> ListOfSpawnPoints
    {
        get { return listOfSpawnPoints; }
        set { listOfSpawnPoints = value; OnValueChanged?.Invoke(); }
    }
}
