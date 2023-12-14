using System;
using System.Collections;
using UnityEngine;

public class OR : MonoBehaviour, ILogicGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] {
        ORFunction
    };
    public StateFunction[] stateFunctions => new StateFunction[] {};
    public int inputsLength => 2;
    public int outputsLength => 1;
    public int stateLength => 0;

    private bool ORFunction(BitArray inputs)
    {
        return inputs[0] | inputs[1];
    }
}
