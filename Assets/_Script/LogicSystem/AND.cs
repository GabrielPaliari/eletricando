using System;
using System.Collections;
using UnityEngine;

public class AND : MonoBehaviour, ILogicGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] {
        ANDFunction
    };
    public StateFunction[] stateFunctions => new StateFunction[] {};
    public int inputsLength => 2;
    public int outputsLength => 1;
    public int stateLength => 0;

    private bool ANDFunction(BitArray inputs)
    {
        Debug.Log($"i0 {inputs[0]}, {inputs[1]}");
        return inputs[0] && inputs[1];
    }
}
