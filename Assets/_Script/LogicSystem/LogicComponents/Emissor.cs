using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emissor : MonoBehaviour, ILogicGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] {
        OutputCurrentState
    };
    public StateFunction[] stateFunctions => new StateFunction[] {};

    public int inputsLength => 0;
    public int outputsLength => 1;
    public int stateLength => 0;

    private bool isTrue = false;
    private int currentIndex = 0;

    private LogicGate logicGate;


    void Start()
    {
        logicGate = GetComponent<LogicGate>();
    }

    public void UpdateOutput()
    {
        if (logicGate.signalSequence.Count > 0)
        {
            isTrue = logicGate.signalSequence[currentIndex % logicGate.signalSequence.Count] > 0;
            currentIndex++;
        }
    }

    private bool OutputCurrentState(BitArray inputs)
    {
        return isTrue;
    }

}
