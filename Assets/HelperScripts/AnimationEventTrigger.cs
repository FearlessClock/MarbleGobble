using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class StringUnityEvent: UnityEvent<string> { }
public class AnimationEventTrigger : MonoBehaviour
{
    public StringUnityEvent OnReceivedEvent;
    public void TriggerEvent(string eventName)
    {
        OnReceivedEvent?.Invoke(eventName);
    }
}
