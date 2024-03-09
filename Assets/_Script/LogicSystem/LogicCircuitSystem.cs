using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;

public class LogicCircuitSystem : MonoBehaviour
{
    private int currentComponentId = 0;
    private static LogicCircuitSystem instance;
    private List<LogicGate> _logicGates = new List<LogicGate>();
    private Dictionary<int, Dictionary<int, UnityEvent<byte>>> outputEvents;
    private Dictionary<int, Dictionary<int, List<LogicGate>>> _outputWires;
    private Dictionary<int, Dictionary<int, List<LogicGate>>> _inputWires;

    public List<LogicGate> logicGates => _logicGates;

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
        outputEvents = new Dictionary<int, Dictionary<int, UnityEvent<byte>>>();
        _outputWires = new Dictionary<int, Dictionary<int, List<LogicGate>>>();
        _inputWires = new Dictionary<int, Dictionary<int, List<LogicGate>>>();
    }

    public void UpdateLogicGates()
    {
        _logicGates.ForEach(logicGate => {
            logicGate.UpdateGate();
        });
    }

    public int AddComponent(LogicGate logicGate)
    {
        if (logicGate.id != 0)
        {
            currentComponentId = logicGate.id;
        } else
        {
            currentComponentId++;
        }
        outputEvents[currentComponentId] = new Dictionary<int, UnityEvent<byte>>();
        _logicGates.Add(logicGate);
        _outputWires[currentComponentId] = new Dictionary<int, List<LogicGate>>();
        _inputWires[currentComponentId] = new Dictionary<int, List<LogicGate>>();
        return currentComponentId;
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
            _inputWires[component][i] = new List<LogicGate>();
        }
        for (int j = 0; j < outputsLength; j++) {
            _outputWires[component][j] = new List<LogicGate>();
        }
    }

    public void RegisterOutputListener(int outputGateId, int outputIndex, int inputGateId, int inputIndex, LogicGate wire)
    {
        UnityEvent<byte> outputEmitter = outputEvents[outputGateId][outputIndex];

        _inputWires[inputGateId][inputIndex].Add(wire);
        _outputWires[outputGateId][outputIndex].Add(wire);

        var inputGate = _logicGates.First((gate) => gate.id == inputGateId);
        outputEmitter.AddListener((outValue) => wire.OnInputChange(0, outValue));
        outputEmitter.AddListener((outValue) => inputGate.OnInputChange(inputIndex, outValue));
        var outputGate = _logicGates.First((gate) => gate.id == outputGateId);
        outputGate.UpdateGate();
    }

    public void UnregisterComponent(LogicGate logicGate)
    {
        if (logicGate != null && logicGate.gameObject != null)
        {
            _logicGates.Remove(logicGate);
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
        if (_outputWires.ContainsKey(logicGate.id))
        {
            var allOutputsWires = _outputWires[logicGate.id];
            allOutputsWires.ToList().ForEach((outputWires) => {
                outputWires.Value.ForEach((wire) =>
                {
                    UnregisterComponent(wire);
                });
            });
            _outputWires.Remove(logicGate.id);
        }
        if (_inputWires.ContainsKey(logicGate.id))
        {
            var allOutputsWires = _inputWires[logicGate.id];
            allOutputsWires.ToList().ForEach((inputWires) => {
                inputWires.Value.ForEach((wire) =>
                {
                    UnregisterComponent(wire);
                });
            });
            _inputWires.Remove(logicGate.id);
        }
    }
}
