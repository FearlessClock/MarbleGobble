using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    [SerializeField] private EntrancePointsHolder entrancePointHolder = null;
    [SerializeField] private float offset = 1.29f;
    [SerializeField] private int angleBetweenPipes = 7;
    private List<int> usedAngles = new List<int>(); 
    private void Awake()
    {
        entrancePointHolder.ListOfEntrancePoints.Clear();
    }

    private void Start()
    {
        CreateNewBranch(0);
    }

    public void AddRandomBranch()
    {
        int angle = Random.Range(0, 360);
        int quadrentIndex = Mathf.FloorToInt(angle / angleBetweenPipes);
        while (usedAngles.Contains(quadrentIndex))
        {
            angle = Random.Range(0, 360);
            quadrentIndex = Mathf.FloorToInt(angle / angleBetweenPipes);
        }
        CreateNewBranch(quadrentIndex * angleBetweenPipes);
    }

    private void CreateNewBranch(int angle)
    {
        entrancePointHolder.CreateNewEntrancePoint(angle, offset);
        usedAngles.Add(angle);
    }
}
