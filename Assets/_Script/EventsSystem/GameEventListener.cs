using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object> { }

public class GameEventListener : MonoBehaviour
{

    [Tooltip("Event to register.")]
    public GameEvent gameEvent;

    [Tooltip("Callback to invoke when Event with GameData is raised.")]
    public CustomGameEvent callback;

    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(Component sender, object data)
    {
        callback.Invoke(sender, data);
    }

}
