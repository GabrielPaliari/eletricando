using UnityEngine;

public interface ILogicGateSpec 
{
    OutputFunction[] outputFunctions { get; }
    StateFunction[] stateFunctions { get; }
    int inputsLength { get; }
    int outputsLength { get; }
    int stateLength { get; }
}
