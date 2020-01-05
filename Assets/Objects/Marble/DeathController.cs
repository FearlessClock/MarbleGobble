using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class DeathController : MonoBehaviour
{
    public UnityEvent OnLoseHealth;
    [SerializeField] private FloatVariable deathCircleRadius = null;
    [SerializeField] private float offsetFromCircleRadius = 0.5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Marble"))
        {
            collision.GetComponent<MarbleController>().RunDestroyAnimation();
            OnLoseHealth?.Invoke();
        }
    }

    private void OnEnable()
    {
        GetComponent<CircleCollider2D>().radius = deathCircleRadius.value - offsetFromCircleRadius;
    }
}
