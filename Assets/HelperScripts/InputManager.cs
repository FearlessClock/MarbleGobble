using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
public class InputManager: MonoBehaviour
{

    public static bool InputExistsContinuous()
    {
        return (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButton(0)) ||
                (Input.touchCount > 0 &&
                (Input.touches[0].phase == TouchPhase.Began ||
                 Input.touches[0].phase == TouchPhase.Moved ||
                 Input.touches[0].phase == TouchPhase.Stationary) &&
                !EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId));
    }

    public static bool InputExistsMoved()
    {
        return (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButton(0)) ||
                (Input.touchCount > 0 &&
                (Input.touches[0].phase == TouchPhase.Moved) &&
                !EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId));
    }

    public static bool InputExistsUp()
    {
        return (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonUp(0)) ||
                (Input.touchCount > 0 &&
                (Input.touches[0].phase == TouchPhase.Ended) &&
                !EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId));   // IsPointerOverGameobject only works with touchPhase Begin, workaround is to check if the pointer is over a UI element at down and save the value
    }

    public static bool InputExistsDown()
    {
#if UNITY_EDITOR
        return (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0)) || 
                (Input.touchCount > 0 &&
                (Input.touches[0].phase == TouchPhase.Began) &&
                !EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId)); ;
#else
        return (Input.touchCount > 0 &&
                (Input.touches[0].phase == TouchPhase.Began) &&
                !EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId));
#endif
    }



    public static Vector3 GetInput(int id)
    {
        if (Application.isMobilePlatform)
        {
            return Input.touches[id].position;
        }
        else
        {
            return Input.mousePosition;
        }
    }
}