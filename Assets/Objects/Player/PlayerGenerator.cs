using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    [SerializeField] private GameStateVariable gameState = null;
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
        StartCoroutine(WaitForGameStart());
    }

    private IEnumerator WaitForGameStart()
    {
        while (gameState.value != GameStateVariable.GameState.Running)
        {
            yield return new WaitForSeconds(0.5f);
        }
        CreateNewBranch(0);
    }

    public void AddRandomBranch()
    {
        if(360 / angleBetweenPipes > entrancePointHolder.ListOfEntrancePoints.Count )
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
    }

    private void CreateNewBranch(int angle)
    {
        entrancePointHolder.CreateNewEntrancePoint(angle, offset);
        usedAngles.Add(angle);
    }
}
