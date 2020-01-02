using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private Image[] lives = null;
    [SerializeField] private Color aliveColor = Color.green;
    [SerializeField] private Color deadColor = Color.red;

    private void Awake()
    {
        foreach (Image ren in lives)
        {
            ren.color = aliveColor;
        }    
    }

    public void UpdateLives(int value)
    {
        for (int i = 0; i < value; i++)
        {
            lives[i].color = aliveColor;
        }
        for (int i = value; i < lives.Length; i++)
        {
            lives[i].color = deadColor;
        }
    }
}
