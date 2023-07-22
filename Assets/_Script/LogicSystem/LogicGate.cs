using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public delegate bool StateFunction(BitArray inputs);
public delegate bool OutputFunction(BitArray inputs);

public class LogicGate : MonoBehaviour
{
    public int id;
    private BitArray inputsValues = new BitArray(0);
    private BitArray outputsValues = new BitArray(0);
    private BitArray stateValues = new BitArray(0);
    private UnityEvent<bool>[] outputEmitters;
    
    private OutputFunction[] outputFunctions;
    private StateFunction[] stateFunctions;
    private int inputsLength = 2;
    private int outputsLength = 1;
    private int stateLength = 1;

    public void OnInputChange(int inputIndex, bool value)
    {
        inputsValues[inputIndex] = value;
        UpdateOutputs();
        UpdateState();
    }

    private void Start()
    {
        inputsValues.Length = inputsLength;
        outputsValues.Length = outputsLength;
        stateValues.Length = stateLength;
        outputEmitters = new UnityEvent<bool>[outputsLength];
        stateFunctions = new StateFunction[stateLength];
        outputFunctions = new OutputFunction[outputsLength];

        id = LogicCircuitSystem.Instance.AddComponent(name);
        RegisterOutputs();
    }

    private void RegisterOutputs()
    {
        for (int oIndex = 0; oIndex < outputsLength; oIndex++)
        {
            var emitter = LogicCircuitSystem.Instance.RegisterOutputEmitter(id, oIndex);
            outputEmitters[oIndex] = emitter;
        }
    }

    private void OnDestroy()
    {
        LogicCircuitSystem.Instance.UnregisterComponent(id);
    }

    private void UpdateOutputs()
    {
        for (int n = 0; 0 < outputsValues.Length; n++)
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
        for (int n = 0; 0 < stateFunctions.Length; n++)
        {
            var newState = stateFunctions[n](inputsValues);
            if (stateValues[n] != newState)
            {
                stateValues[n] = newState;
            }
        }
    }
}
