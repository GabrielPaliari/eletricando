using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LEDReceptor : MonoBehaviour, ILogicGateSpec, ISignalSeqGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] { };
    public StateFunction[] stateFunctions => new StateFunction[] {
        OnOffState
    };
    public int inputsLength => 1;
    public int outputsLength => 0;
    public int stateLength => 1;

    [SerializeField]
    private MeshRenderer lightMeshRenderer;
    [SerializeField]
    private Material onMaterial;
    [SerializeField]
    private Material offMaterial;

    private SignalComponentData _signalComponent;

    [SerializeField]
    private TextMeshPro _nameTMP;
    public TextMeshPro nameTMP => _nameTMP;

    public ESignalComponent compType => ESignalComponent.Receptor;

    public List<int> _signalSequence => new List<int>();

    private int currentValue = 0;
    private int currentIndex = 0;

    void Start()
    {
        lightMeshRenderer.material = offMaterial;
    }
    public void Initialize(int id, BuildedComponentSO buildedComponent)
    {
        _signalComponent = new SignalComponentData(id, compType, buildedComponent.signalSequence, 0, 0);
        LevelSignalsManager.Instance.RegisterSignalComponent.Invoke(_signalComponent);
        nameTMP.text = buildedComponent.componentName != "" ? buildedComponent.componentName : _signalComponent.displayName;
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

    private bool OnOffState(BitArray inputs)
    {
        var isOn = inputs[0];
        currentValue = isOn ? 1 : 0;
        if (lightMeshRenderer != null)
        {
            lightMeshRenderer.material = isOn ? onMaterial : offMaterial;
        }
        UpdateAttempt();
        return inputs[0];
    }
}
