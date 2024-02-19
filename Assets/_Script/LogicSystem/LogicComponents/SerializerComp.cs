using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializerComp : MonoBehaviour, ILogicGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] {
        SerializeFunction
    };
    public StateFunction[] stateFunctions => new StateFunction[] { };
    public int inputsLength => 8;
    public int outputsLength => 1;
    public int stateLength => 0;

    private byte SerializeFunction(byte[] inputs)
    {
        byte result = 0;
        for (int i = 0; i < inputsLength; i++)
        {
            var bit = inputs[i] & 1;
            result += (byte)(bit << i);
        }
        return result;

    }
}
