using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GOEvent: UnityEvent<GameObject> { }

public class GOEventListener : MonoBehaviour
{
    public GameEvent Event;
    public GOEvent Response;

    public void OnEventRaised()
    {
        Response.Invoke(gameObject);
    }
}