using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public struct EntrancePoint
{
    public int ID;
    public Vector2 entrancePoint;
    public Vector2 exitPoint;
    public int angleFromRight;
}

[CreateAssetMenu(fileName = "New Entrance Point Holder", menuName = "MarbleGobble/Entrance Point Holder")]
public class EntrancePointsHolder : ScriptableObject
{
    public List<EntrancePoint> listOfEntrancePoints = new List<EntrancePoint>();
    public Action OnValueChanged;
    private static int idCounter = 0;

    public List<EntrancePoint> ListOfEntrancePoints
    {
        get { return listOfEntrancePoints; }
        set { listOfEntrancePoints = value; OnValueChanged?.Invoke(); }
    } 

    public void CreateNewEntrancePoint(int angle, float offset)
    {
        EntrancePoint newPoint = new EntrancePoint();
        Vector2 exitPoint = Vector2.zero;
        Vector2 entrancePoint;
        entrancePoint = (Quaternion.Euler(0, 0, angle) * Vector2.right).normalized * offset;
        if (listOfEntrancePoints.Count > 0)
        {
            EntrancePoint branch = listOfEntrancePoints[Random.Range(0, listOfEntrancePoints.Count)];
            exitPoint = branch.entrancePoint - branch.exitPoint;
            // Get a random pos between the end and the start
            exitPoint *=  Random.Range(0.3f, 0.7f);
            exitPoint += branch.exitPoint;
            Debug.Log(exitPoint);
        }
        newPoint = new EntrancePoint()
        {
            angleFromRight = angle,
            entrancePoint = entrancePoint,
            exitPoint = exitPoint,
            ID = idCounter++
        };
        listOfEntrancePoints.Add(newPoint);
        OnValueChanged?.Invoke();

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
            entrancePoint = Vector2.zero,
            exitPoint = Vector2.zero,
            angleFromRight = 0
        };
    }
}
