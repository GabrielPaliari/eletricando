using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class LogicCircuitSystem : MonoBehaviour
{
    private static LogicCircuitSystem instance;
    private Dictionary<int, Dictionary<int, UnityEvent<bool>>> outputEvents;

    public static LogicCircuitSystem Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        outputEvents = new Dictionary<int, Dictionary<int, UnityEvent<bool>>>();
    }

    public int AddComponent(string name)
    {
        int id = name.GetHashCode();
        if (!outputEvents.ContainsKey(id))
        {
            outputEvents[id] = new Dictionary<int, UnityEvent<bool>>();
        }
        return id;
    }

    public UnityEvent<bool> RegisterOutputEmitter(int component, int outputIndex)
    {
        if (!outputEvents.ContainsKey(component))
        {
            outputEvents[component] = new Dictionary<int, UnityEvent<bool>>();
        }

        UnityEvent<bool> emitter = null;
        if (!outputEvents[component].ContainsKey(outputIndex))
        {
            emitter = new UnityEvent<bool>();   
            outputEvents[component][outputIndex] = emitter;
        }
        return emitter;
    }

    public void RegisterOutputListener(LogicGate outputGate, int outputIndex, LogicGate inputGate, int inputIndex)
    {
        UnityEvent<bool> emitter = outputEvents[outputGate.id][outputIndex];
        emitter.AddListener((outValue) => inputGate.OnInputChange(inputIndex, outValue));
    }

    public void UnregisterComponent(int component)
    {
        outputEvents.Remove(component);
    }
}
