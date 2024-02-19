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
    private Dictionary<int, Dictionary<int, UnityEvent<byte>>> outputEvents;
    private Dictionary<int, Dictionary<int, List<LogicGate>>> outputWires;
    private Dictionary<int, Dictionary<int, List<LogicGate>>> inputWires;

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
        }
    }

    private void Start()
    {
        outputEvents = new Dictionary<int, Dictionary<int, UnityEvent<byte>>>();
        outputWires = new Dictionary<int, Dictionary<int, List<LogicGate>>>();
        inputWires = new Dictionary<int, Dictionary<int, List<LogicGate>>>();
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
            outputEvents[id] = new Dictionary<int, UnityEvent<byte>>();
            logicGates.Add(logicGate);
        }
        if (!outputWires.ContainsKey(id))
        {
            outputWires[id] = new Dictionary<int, List<LogicGate>>();
        }
        if (!inputWires.ContainsKey(id))
        {
            inputWires[id] = new Dictionary<int, List<LogicGate>>();
        }
        return id;
    }

    public UnityEvent<byte> RegisterOutputEmitter(int component, int outputIndex)
    {
        if (!outputEvents.ContainsKey(component))
        {
            outputEvents[component] = new Dictionary<int, UnityEvent<byte>>();
        }

        UnityEvent<byte> emitter = null;
        if (!outputEvents[component].ContainsKey(outputIndex))
        {
            emitter = new UnityEvent<byte>();   
            outputEvents[component][outputIndex] = emitter;
        }
        return emitter;
    }

    public void RegisterWireConectors(int component, int inputsLength, int outputsLength)
    {
        for (int i = 0; i < inputsLength; i++) {
            inputWires[component][i] = new List<LogicGate>();
        }
        for (int j = 0; j < outputsLength; j++) {
            outputWires[component][j] = new List<LogicGate>();
        }
    }

    public void RegisterOutputListener(LogicGate outputGate, int outputIndex, LogicGate inputGate, int inputIndex, LogicGate wire)
    {
        UnityEvent<byte> outputEmitter = outputEvents[outputGate.id][outputIndex];
        outputEmitter.AddListener((outValue) => wire.OnInputChange(0, outValue));

        inputWires[inputGate.id][inputIndex].Add(wire);
        outputWires[outputGate.id][outputIndex].Add(wire);

        UnityEvent<byte> wireEmitter = outputEvents[wire.id][0];
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
                emitter.Value.Invoke(0);
                emitter.Value.RemoveAllListeners();
            });
            outputEvents.Remove(logicGate.id);
        }
        if (outputWires.ContainsKey(logicGate.id))
        {
            var allOutputsWires = outputWires[logicGate.id];
            allOutputsWires.ToList().ForEach((outputWires) => {
                outputWires.Value.ForEach((wire) =>
                {
                    UnregisterComponent(wire);
                });
            });
            outputWires.Remove(logicGate.id);
        }
        if (inputWires.ContainsKey(logicGate.id))
        {
            var allOutputsWires = inputWires[logicGate.id];
            allOutputsWires.ToList().ForEach((inputWires) => {
                inputWires.Value.ForEach((wire) =>
                {
                    UnregisterComponent(wire);
                });
            });
            inputWires.Remove(logicGate.id);
        }
    }
}
