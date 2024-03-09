using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class NOT : MonoBehaviour, ILogicGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] {
        NOTFunction
    };
    public int inputsLength => 1;
    public int outputsLength => 1;

    private byte NOTFunction(byte[] inputs)
    {
        var num = inputs[0];
        if (num == 0)
        {
            return 1;
        }
        var bits = (int)Math.Ceiling(Math.Log(num + 1, 2));
        var mask = (1 << bits) - 1;
        return (byte)((byte)~num & (byte)mask);

    }
}
