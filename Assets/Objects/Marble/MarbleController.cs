using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MarbleController : MonoBehaviour
{
    private Vector2 target ;
    public Vector2 Target { set { target = value; } }
    private Vector2 startingTarget;
    public Vector2 StartingTarget { set { startingTarget = value; } }
    [SerializeField] private float movementSpeed = 1;
    private Vector2 direction;
    private Rigidbody2D rb = null;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        this.transform.position = startingTarget;
        direction = (target - startingTarget).normalized;
    }

    private void Update()
    {
        rb.MovePosition((Vector2)this.transform.position + direction * movementSpeed * Time.deltaTime);
    }
}
