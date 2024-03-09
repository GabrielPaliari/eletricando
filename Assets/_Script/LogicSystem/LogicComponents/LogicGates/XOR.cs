using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XOR : MonoBehaviour, ILogicGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] {
        XORFunction
    };
    public int inputsLength => 2;
    public int outputsLength => 1;

    private byte XORFunction(byte[] inputs)
    {
        return (byte)(inputs[0] ^ inputs[1]);
    }
}
