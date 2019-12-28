using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    [SerializeField] private EntrancePointsHolder entrancePointHolder = null;
    [SerializeField] private float offset = 1.29f;
    private List<int> usedAngles = new List<int>(); 
    private void Awake()
    {
        entrancePointHolder.ListOfEntrancePoints.Clear();
    }

    private void Start()
    {
        CreateNewBranch(0);
        Invoke("AddRandomBranch", 5);
        Invoke("AddRandomBranch", 10);
        Invoke("AddRandomBranch", 15);
    }

    public void AddRandomBranch()
    {
        int angle = Random.Range(0, 360);
        while (usedAngles.Contains(angle))
        {
            angle = Random.Range(0, 360);
        }
        CreateNewBranch(angle);
    }

    private void CreateNewBranch(int angle)
    {
        entrancePointHolder.CreateNewEntrancePoint(angle, offset);
        usedAngles.Add(angle);
    }
}
