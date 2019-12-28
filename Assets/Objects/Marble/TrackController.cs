using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    public Vector2 startingPoint;
    public float angle = 0;

    private void OnEnable()
    {
        this.transform.position = startingPoint;
        this.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
