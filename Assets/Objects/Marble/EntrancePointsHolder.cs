using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EntrancePoint
{
    public int ID;
    public Vector2 spawnPoint;
    public int angleFromRight;
}

[CreateAssetMenu(fileName = "New Entrance Point Holder", menuName = "MarbleGobble/Entrance Point Holder")]
public class EntrancePointsHolder : ScriptableObject
{
    public List<EntrancePoint> listOfEntrancePoints = null;
    public Action OnValueChanged;

    public List<EntrancePoint> ListOfEntrancePoints
    {
        get { return listOfEntrancePoints; }
        set { listOfEntrancePoints = value; OnValueChanged?.Invoke(); }
    }

    public EntrancePoint GetEntrancePoint(int ID)
    {
        foreach (EntrancePoint entrancePoint in listOfEntrancePoints)
        {
            if (entrancePoint.ID == ID)
            {
                return entrancePoint;
            }
        }

        Debug.Log("Entrance point with ID:" + ID + " wasn't found in list");

        return new EntrancePoint
        {
            ID = -1,
            spawnPoint = Vector2.zero,
            angleFromRight = 0
        };
    }
}
