using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AwakeEvent : MonoBehaviour
{
    public UnityEvent OnAwake;

    private void Awake()
    {
        OnAwake?.Invoke();
    }
}
