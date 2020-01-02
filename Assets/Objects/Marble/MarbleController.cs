using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class MarbleController : MonoBehaviour
{
    [SerializeField] private GameStateVariable gamestate = null;
    private Vector2 target ;
    public Vector2 Target { set { target = value; } }
    private Vector2 startingTarget;
    public Vector2 StartingTarget { set { startingTarget = value; } }
    private float currentMovementSpeed = 0;
    [SerializeField] private float initMovementSpeed = 0;
    [SerializeField] private float maxMovementSpeed = 1;
    [SerializeField] private float timeToMaxSpeed = 1;
    private float currentTimeToMaxSpeed = 0;
    private bool isMoving = false;
    private bool isDieing = false;
    private Vector2 direction;
    private Rigidbody2D rb = null;

    public UnityEvent OnSpawn;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        OnSpawn?.Invoke();
        this.transform.position = startingTarget;
        direction = (target - startingTarget).normalized;
    }

    private void Update()
    {
        if(isMoving)
        {
            if(!isDieing)
            {
                if (currentTimeToMaxSpeed <= timeToMaxSpeed)
                {
                    currentTimeToMaxSpeed += Time.deltaTime;

                    currentMovementSpeed = initMovementSpeed + (currentTimeToMaxSpeed / timeToMaxSpeed) * (maxMovementSpeed - initMovementSpeed);
                }
                else
                {
                    currentMovementSpeed = maxMovementSpeed;
                }
            }

            if (gamestate.value == GameStateVariable.GameState.Running)
            {
                rb.MovePosition((Vector2)this.transform.position + direction * currentMovementSpeed * Time.deltaTime);
            }
        }
    }

    public void AnimationHandler(string eventId)
    {
        switch (eventId)
        {
            case "EndSpawnAnimation":
                isMoving = true;
                break;
            case "DestroyObject":
                Destroy();
                break;
            default:
                break;
        }
    }

    public void RunDestroyAnimation()
    {
        GetComponent<Animator>().SetTrigger("DestroyTrigger");
        currentMovementSpeed *= 0.3f;
        this.tag = "Untagged";
        isDieing = true;
    }

    public void RunCatchAnimation()
    {
        GetComponent<Animator>().SetTrigger("CatchTrigger");
        currentMovementSpeed *= 0.1f;
        this.tag = "Untagged";
        isDieing = true;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
