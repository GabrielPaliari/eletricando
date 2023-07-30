using System.Collections;
using UnityEngine;

public class Wire : MonoBehaviour, ILogicGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] {
        OnOffState
    };
    public StateFunction[] stateFunctions => new StateFunction[] {
        OnOffState
    };
    public int inputsLength => 1;
    public int outputsLength => 1;
    public int stateLength => 1;

    private bool OnOffState(BitArray inputs)
    {
        if (inputs[0])
        {
            Debug.Log("fio ligado");
        }
        else
        {
            Debug.Log("fio desligado");
        }
        return inputs[0];
    }
}
