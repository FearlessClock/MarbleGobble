using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrancePointController : MonoBehaviour
{
    private ScoreController scoreController = null;
    private void Awake()
    {
        scoreController = FindObjectOfType<ScoreController>();
        if(scoreController == null)
        {
            Debug.LogError("Score controller doesn't exist, please add it to the scene");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        scoreController.AddToScore(1);
        Destroy(collision.gameObject);
    }
}
