using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LoadScene), typeof(AddScene), typeof(UnloadSceneCallback))]
public class FadeHandler : MonoBehaviour
{
    private LoadScene loadScene = null;
    private AddScene addScene = null;
    private UnloadSceneCallback unloadScene = null;
    [SerializeField] private StringVariable sceneToLoad = null;
    [SerializeField] private StringVariable fadeSceneToUnload = null;
    private string[] activeScenes = new string[0];
    private void Awake()
    {
        loadScene = GetComponent<LoadScene>();
        addScene = GetComponent<AddScene>();
        unloadScene = GetComponent<UnloadSceneCallback>();
        if (SceneManager.GetSceneByName(sceneToLoad.value).buildIndex > 0)
        {
            try
            {
                SceneManager.UnloadSceneAsync(sceneToLoad.value);
            }
            catch
            {
                Debug.LogWarning("Could not unload " + sceneToLoad.value);
            }
        }
        activeScenes = new string[SceneManager.sceneCount];
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (!SceneManager.GetSceneAt(i).name.Equals(sceneToLoad))
            {
                activeScenes[i] = SceneManager.GetSceneAt(i).name;
            }
        }
    }
    public void HandleEvent(string eventName)
    {
        if (eventName.ToUpper().Equals("FADEDONE"))
        {
            unloadScene.UnloadScene(fadeSceneToUnload);
        }
        else if (eventName.ToUpper().Equals("FADEHALFWAY"))
        {
            foreach (string sceneName in activeScenes)
            {
                unloadScene.UnloadScene(sceneName);
            }
            addScene.AddSceneCallback(sceneToLoad);
        }
    }
}
