public interface ILogicGateSpec 
{
    OutputFunction[] outputFunctions { get; }
    int inputsLength { get; }
    int outputsLength { get; }    
}
