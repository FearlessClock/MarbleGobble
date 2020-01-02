using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyMobileInit : MonoBehaviour
{
    // Checks if EM has been initialized and initialize it if not.
    // This must be done once before other EM APIs can be used.
    void Awake()
    {
        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();
    }
}
