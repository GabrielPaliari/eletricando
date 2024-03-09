using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate byte StateFunction(byte[] inputs);
public delegate byte OutputFunction(byte[] inputs);

public class LogicGate : MonoBehaviour
{
    public int id;
    public ILogicGateSpec specification;
    
    private byte[] inputsValues;
    
    private byte[] outputsValues;
    private OutputFunction[] outputFunctions;
    private UnityEvent<byte>[] outputEmitters;

    public int componentId;
    public Vector3Int position;
    public RotationDir rotationDir;
    public List<Vector3> wireNodes = new();
    public Vector2Int connectorA;
    public Vector2Int connectorB;

    public void OnInputChange(int inputIndex, byte value)
    {
        if (inputsValues != null) {
            inputsValues[inputIndex] = value;
        }
        UpdateGate();
    }

    public void Initialize(int compId, Vector3Int pos, RotationDir rot, int buildedId = 0, int initialState = 0)
    {
        id = buildedId;
        componentId = compId;
        position = pos;
        rotationDir = rot;
        Initialize(initialState);
    }

    public void Initialize(List<Vector3> nodes, Vector2Int connA, Vector2Int connB)
    {
        componentId = 0;
        wireNodes = nodes;
        connectorA = connA;
        connectorB = connB;
        Initialize();
    }

    private void Initialize(int initialState = 0)
    {
        specification = GetComponent<ILogicGateSpec>();
        if (specification is IStateGateSpec)
        {
            (specification as IStateGateSpec).InitState(initialState);
        }

        if (specification != null)
        {
            id = LogicCircuitSystem.Instance.AddComponent(this);

            inputsValues = new byte[(specification.inputsLength)];

            outputsValues = new byte[(specification.outputsLength)];
            outputFunctions = specification.outputFunctions;
            outputEmitters = new UnityEvent<byte>[specification.outputsLength];

            RegisterOutputs();
            LogicCircuitSystem.Instance.RegisterWireConectors(id, specification.inputsLength, specification.outputsLength);
        }
        else
        {
            Debug.LogError("No ILogicGateSpec found in prefab");
        }
    }

    private void RegisterOutputs()
    {
        for (int oIndex = 0; oIndex < outputFunctions.Length; oIndex++)
        {
            var emitter = LogicCircuitSystem.Instance.RegisterOutputEmitter(id, oIndex);
            outputEmitters[oIndex] = emitter;
        }
    }

    private void OnDestroy()
    {
        LogicCircuitSystem.Instance.UnregisterComponent(this);
    }

    public void UpdateGate()
    {
        UpdateOutputs();
    }

    private void UpdateOutputs()
    {
        if (outputFunctions == null || outputFunctions.Length == 0) return;
        for (int n = 0; n < outputFunctions.Length; n++)
        {
            var outputFunction = outputFunctions[n];
            var newOutput = outputFunction(inputsValues);
            outputsValues[n] = newOutput;
            outputEmitters[n].Invoke(newOutput);
        }
    }
}