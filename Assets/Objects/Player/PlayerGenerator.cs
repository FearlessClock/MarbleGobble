using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    [SerializeField] private GameStateVariable gameState = null;
    [SerializeField] private EntrancePointsHolder entrancePointHolder = null;
    [SerializeField] private FloatVariable offset = null;
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
            int limit = 1000;
            while (usedAngles.Contains(quadrentIndex) && limit-- > 0)
            {
                angle = Random.Range(0, 360);
                quadrentIndex = Mathf.FloorToInt(angle / angleBetweenPipes);
            }
            if(limit > 0)
            {
                CreateNewBranch(quadrentIndex * angleBetweenPipes);
            }

        }
    }

    private void CreateNewBranch(int angle)
    {
        entrancePointHolder.CreateNewEntrancePoint(angle, offset);
        usedAngles.Add(angle);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, offset);
    }
}
