using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathController : MonoBehaviour
{
    public UnityEvent OnLoseHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Marble"))
        {
            Destroy(collision.gameObject);
            OnLoseHealth?.Invoke();
        }
    }
}
