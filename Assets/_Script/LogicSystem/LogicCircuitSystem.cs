using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;

public class LogicCircuitSystem : MonoBehaviour
{
    private int currentComponentId = 0;
    private static LogicCircuitSystem instance;
    private List<LogicGate> logicGates = new List<LogicGate>();
    private Dictionary<int, Dictionary<int, UnityEvent<bool>>> outputEvents;
    private Dictionary<int, Dictionary<int, List<LogicGate>>> wiresConnected;

    public static LogicCircuitSystem Instance => instance;

    public event Action OnClicked;
    
    private void Update()
    {
        // TODO: Transformar input system em um singleton e chamar aqui
        if (Input.GetMouseButtonDown(0))
            OnClicked?.Invoke();
    }
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
        wiresConnected = new Dictionary<int, Dictionary<int, List<LogicGate>>>();
    }

    public void UpdateLogicGates()
    {
        logicGates.ForEach(logicGate => {
            logicGate.UpdateGate();
        });
    }

    public int AddComponent(LogicGate logicGate)
    {
        currentComponentId++;
        int id = currentComponentId;
        if (!outputEvents.ContainsKey(id))
        {
            outputEvents[id] = new Dictionary<int, UnityEvent<bool>>();
            logicGates.Add(logicGate);

        }
        if (!wiresConnected.ContainsKey(id))
        {
            wiresConnected[id] = new Dictionary<int, List<LogicGate>>();
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

    public void RegisterWireConectors(int component, int inputsLength, int outputsLength)
    {
        // TODO: separar na memória as entradas das saídas
        for (int i = 0; i < inputsLength; i++) {
            wiresConnected[component][i] = new List<LogicGate>();
        }
        for (int j = 0; j < outputsLength; j++) { 
            wiresConnected[component][inputsLength+j] = new List<LogicGate>();
        }
    }

    public void RegisterOutputListener(LogicGate outputGate, int outputIndex, LogicGate inputGate, int inputIndex, LogicGate wire)
    {
        UnityEvent<bool> outputEmitter = outputEvents[outputGate.id][outputIndex];
        outputEmitter.AddListener((outValue) => wire.OnInputChange(0, outValue));

        wiresConnected[inputGate.id][inputIndex].Add(wire);

        UnityEvent<bool> wireEmitter = outputEvents[wire.id][0];
        wireEmitter.AddListener((outValue) => inputGate.OnInputChange(inputIndex, outValue));
    }

    public void UnregisterComponent(LogicGate logicGate)
    {
        if (logicGate != null && logicGate.gameObject != null)
        {
            Destroy(logicGate.gameObject);
        } else
        {
            return;
        }
        if (outputEvents.ContainsKey(logicGate.id))
        {
            var componentOutputsEvents = outputEvents[logicGate.id];
            componentOutputsEvents.ToList().ForEach((emitter) => {
                emitter.Value.Invoke(false);
                emitter.Value.RemoveAllListeners();
            });
            outputEvents.Remove(logicGate.id);
        }
        if (wiresConnected.ContainsKey(logicGate.id))
        {
            // Remove Wires Connected
            var allOutputsWires = wiresConnected[logicGate.id];
            allOutputsWires.ToList().ForEach((outputWires) => {
                outputWires.Value.ForEach((wire) =>
                {
                    UnregisterComponent(wire);
                });
            });
            wiresConnected.Remove(logicGate.id);
        }
    }
}
