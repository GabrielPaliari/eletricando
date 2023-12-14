using System;
using System.Collections;
using UnityEngine;

public class NOT : MonoBehaviour, ILogicGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] {
        NOTFunction
    };
    public StateFunction[] stateFunctions => new StateFunction[] {};
    public int inputsLength => 1;
    public int outputsLength => 1;
    public int stateLength => 0;

    private bool NOTFunction(BitArray inputs)
    {
        return !inputs[0];
    }
}
