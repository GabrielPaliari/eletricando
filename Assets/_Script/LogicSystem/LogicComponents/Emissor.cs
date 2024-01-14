using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Emissor : MonoBehaviour, ILogicGateSpec, ISignalSeqGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] {
        OutputCurrentState
    };
    public StateFunction[] stateFunctions => new StateFunction[] {};

    public int inputsLength => 0;
    public int outputsLength => 1;
    public int stateLength => 0;

    public List<int> _signalSequence => new List<int>();
    public ESignalComponent compType => ESignalComponent.Emissor;

    [SerializeField]
    private TextMeshPro _nameTMP;
    public TextMeshPro nameTMP => _nameTMP;


    private SignalComponentData _signalComponent;

    private bool isTrue = false;
    private int currentIndex = 0;

    public void Initialize(int id, List<int> signalSeq)
    {
        _signalComponent = new SignalComponentData(id, ESignalComponent.Emissor, signalSeq, 0, 0);
        LevelSignalsManager.Instance.RegisterSignalComponent.Invoke(_signalComponent);
        nameTMP.text = _signalComponent.displayName;
    }

    public void UpdateOutput()
    {
        if (_signalComponent.signalSequence.Count > 0)
        {
            var cicledIndex = currentIndex % _signalComponent.signalSequence.Count;
            _signalComponent.currentIndex = cicledIndex;
            _signalComponent.currentValue = isTrue ? 1 : 0;

            isTrue = _signalComponent.signalSequence[cicledIndex] > 0;
            LevelSignalsManager.Instance.UpdateSignalComponent.Invoke(_signalComponent);            
        }
    }

    public void OnClock() {
        currentIndex++;
    }
    private bool OutputCurrentState(BitArray inputs)
    {
        UpdateOutput();
        return isTrue;
    }

}
