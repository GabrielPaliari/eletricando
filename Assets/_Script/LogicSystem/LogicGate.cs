using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public delegate bool StateFunction(BitArray inputs);
public delegate bool OutputFunction(BitArray inputs);

public class LogicGate : MonoBehaviour
{
    public int id;
    private ILogicGateSpec specification;
    
    private BitArray inputsValues;
    
    private BitArray outputsValues;
    private OutputFunction[] outputFunctions;
    private UnityEvent<bool>[] outputEmitters;
    
    private BitArray stateValues;
    private StateFunction[] stateFunctions;

    public void OnInputChange(int inputIndex, bool value)
    {
        if (inputsValues != null) {
            inputsValues[inputIndex] = value;
        }   
    }

    public void Initialize()
    {
        specification = GetComponent<ILogicGateSpec>();
        if (specification != null )
        {
            id = LogicCircuitSystem.Instance.AddComponent(this);
            
            inputsValues = new BitArray(specification.inputsLength);
            
            outputsValues = new BitArray(specification.outputsLength);
            outputFunctions = specification.outputFunctions;
            outputEmitters = new UnityEvent<bool>[specification.outputsLength];

            stateValues = new BitArray(specification.stateLength);
            stateFunctions = specification.stateFunctions;

            RegisterOutputs();
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
        LogicCircuitSystem.Instance.UnregisterComponent(id);
    }

    public void UpdateGate()
    {
        UpdateOutputs();
        UpdateState();
    }

    private void UpdateOutputs()
    {
        if (outputFunctions == null || outputFunctions.Length == 0) return;
        for (int n = 0; n < outputFunctions.Length; n++)
        {
            var newOutput = outputFunctions[n](inputsValues);
            if (outputsValues[n] != newOutput)
            {
                outputsValues[n] = newOutput;
                outputEmitters[n].Invoke(newOutput);
            }
        }
    }
    private void UpdateState()
    {
        if (stateFunctions == null || stateFunctions.Length == 0) return;
        for (int n = 0; n < stateFunctions.Length; n++)
        {
            var stateFunction = stateFunctions[n];
            var newState = stateFunction(inputsValues);
            if (stateValues[n] != newState)
            {
                stateValues[n] = newState;
            }
        }
    }
}