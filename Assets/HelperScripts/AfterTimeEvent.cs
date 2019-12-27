using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AfterTimeEvent : MonoBehaviour
{
    [SerializeField] private float time = 0;
    public UnityEvent OnTimerDone;

    private void OnEnable()
    {
        Invoke("LauchEvent", time);
    }

    private void LauchEvent()
    {
        if (this.isActiveAndEnabled)
        {
            OnTimerDone?.Invoke();
        }
    }
}
