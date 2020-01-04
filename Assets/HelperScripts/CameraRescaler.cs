using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraRescaler : MonoBehaviour
{
    private Camera currentCamera = null;
    [SerializeField] private float sceneWidth = 8f;
    private void Awake()
    {
        currentCamera = GetComponent<Camera>();
        if (currentCamera != null)
        {
            Debug.Log("No camera on the object");
        }
    }
    private void Update()
    {
        currentCamera.orthographicSize = 0.5f * (sceneWidth / Screen.width) * Screen.height;
    }
}
