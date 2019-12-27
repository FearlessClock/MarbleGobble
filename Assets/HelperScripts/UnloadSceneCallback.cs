using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadSceneCallback : MonoBehaviour
{
    public void UnloadScene(StringVariable sceneName)
    {
        UnloadScene(sceneName.value);
    }

    public void UnloadScene(string sceneName)
    {
        Scene toUnload = SceneManager.GetSceneByName(sceneName);
        if (sceneName == null || sceneName == null || toUnload.name == null)
        {
            Debug.Log("Could not unload a scene on object " + name);
        }
        else if (toUnload != null && toUnload.name.Equals(sceneName))
        {
            SceneManager.UnloadSceneAsync(toUnload);
        }
    }
}
