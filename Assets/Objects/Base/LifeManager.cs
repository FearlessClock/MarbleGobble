using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private LifeCapsuleController[] lives = null;

    private void Awake()
    {
    }

    public void UpdateLives(int value)
    {
        for (int i = value; i < lives.Length; i++)
        {
            lives[i].Kill();
        }
    }
}
