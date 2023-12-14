using System;
using System.Collections;
using UnityEngine;

public class XOR : MonoBehaviour, ILogicGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] {
        XORFunction
    };
    public StateFunction[] stateFunctions => new StateFunction[] {};
    public int inputsLength => 2;
    public int outputsLength => 1;
    public int stateLength => 0;

    private bool XORFunction(BitArray inputs)
    {
        return inputs[0] ^ inputs[1];
    }
}
