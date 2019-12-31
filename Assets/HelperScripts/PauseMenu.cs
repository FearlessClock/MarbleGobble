using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameStateVariable stateVariable = null;
    [SerializeField] private bool stopTime = true;
    public UnityEngine.Events.UnityEvent OnPauseMenuActivate;
    public UnityEngine.Events.UnityEvent OnPauseMenuDeactivate;

    public void PauseGame(){
        if (stateVariable == GameStateVariable.GameState.Pause)
        {
            Resume();
            OnPauseMenuDeactivate?.Invoke();
        }
        else if (stateVariable == GameStateVariable.GameState.Running)
        {
            Pause();
            OnPauseMenuActivate?.Invoke();
        }
    }

    private void Resume()
    {
        if (stopTime)
        {
            Time.timeScale = 1;
        }
        stateVariable.SetValue(GameStateVariable.GameState.Running);
    }

    private void Pause()
    {
        if (stopTime)
        {
            Time.timeScale = 0;
        }
        stateVariable.SetValue(GameStateVariable.GameState.Pause);
    }
}
