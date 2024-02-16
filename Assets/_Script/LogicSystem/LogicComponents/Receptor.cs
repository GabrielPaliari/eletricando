using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Receptor : MonoBehaviour, ILogicGateSpec, ISignalSeqGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] {};
    public StateFunction[] stateFunctions => new StateFunction[] {
        UpdateState
    };

    public int inputsLength => 1;
    public int outputsLength => 0;
    public int stateLength => 1;

    public List<int> _signalSequence => new List<int>();
    public ESignalComponent compType => ESignalComponent.Receptor;

    [SerializeField]
    private TextMeshPro _nameTMP;
    public TextMeshPro nameTMP => _nameTMP;


    private SignalComponentData _signalComponent;

    private int currentValue = 0;
    private int currentIndex = 0;

    public void Initialize(int id, BuildedComponentSO buildedComponent)
    {
        _signalComponent = new SignalComponentData(id, compType, buildedComponent.signalSequence, 0, 0, buildedComponent.componentName);
        LevelSignalsManager.Instance.RegisterSignalComponent.Invoke(_signalComponent);
        nameTMP.text = _signalComponent.displayName;
    }

    private void UpdateAttempt()
    {        
        if (_signalComponent.signalSequence.Count > 0)
        {
            var cicledIndex = currentIndex % _signalComponent.signalSequence.Count;
            _signalComponent.Update(cicledIndex, currentValue);

            if (_signalComponent.hasChanged)
            {
                LevelSignalsManager.Instance.UpdateSignalComponent.Invoke(_signalComponent);
            }            
        }
    }

    public void OnClock()
    {
        currentIndex++;
    }

    public bool UpdateState(BitArray inputs)
    {
        currentValue = inputs[0] ? 1 : 0;
        UpdateAttempt();
        return inputs[0];
    }
}
