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

    private byte state = 0;
    private int currentIndex = 0;

    [SerializeField]
    private MeshRenderer lightMeshRenderer;
    [SerializeField]
    private Material onMaterial;
    [SerializeField]
    private Material offMaterial;

    public void Initialize(int id, BuildedComponentSO buildedComponent)
    {
        _signalComponent = new SignalComponentData(id, compType, buildedComponent.signalSequence, 0, 0, buildedComponent.componentName);
        LevelSignalsManager.Instance.RegisterSignalComponent.Invoke(_signalComponent);
        nameTMP.text = _signalComponent.displayName;
    }

    public void UpdateOutput()
    {
        if (_signalComponent.signalSequence.Count > 0)
        {
            var cicledIndex = currentIndex % _signalComponent.signalSequence.Count;
            _signalComponent.currentIndex = cicledIndex;
            state = (byte)_signalComponent.signalSequence[cicledIndex];
            _signalComponent.currentValue = state;

            LevelSignalsManager.Instance.UpdateSignalComponent.Invoke(_signalComponent);            
        }
    }

    public void OnClock() {
        currentIndex++;
    }
    private byte OutputCurrentState(byte[] inputs)
    {
        UpdateOutput();
        if (lightMeshRenderer != null)
        {
            lightMeshRenderer.material = state > 0 ? onMaterial : offMaterial;
        }
        return state;
    }

}
