using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AND : MonoBehaviour, ILogicGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] {
        ANDFunction
    };
    public int inputsLength => 2;
    public int outputsLength => 1;

    private byte ANDFunction(byte[] inputs)
    {
        return (byte)(inputs[0] & inputs[1]);
    }
}
