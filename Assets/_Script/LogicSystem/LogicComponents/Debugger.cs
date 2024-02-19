using System;
using TMPro;
using UnityEngine;

public class Debugger : MonoBehaviour, ILogicGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] { };
    public StateFunction[] stateFunctions => new StateFunction[] {
        DebugState
    };
    public int inputsLength => 1;
    public int outputsLength => 0;
    public int stateLength => 1;

    [SerializeField]
    private TextMeshPro textObj;

    void Start()
    {
        textObj.text = "0";
    }

    private byte DebugState(byte[] inputs)
    {
        string binary = Convert.ToString(inputs[0], 2);
        textObj.text = $"{inputs[0]} ({binary})";
        return inputs[0];
    }
}
