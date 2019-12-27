using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolSetter : MonoBehaviour
{
    [SerializeField] private BoolVariable variable = null;
    [SerializeField] private bool toSetValue = false;
    private void Awake()
    {
        variable.SetValue(toSetValue);
    }
}
