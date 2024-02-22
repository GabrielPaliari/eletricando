using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using TMPro;

public class ConstantEmitter : MonoBehaviour, ILogicGateSpec
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

    private LogicGate logicGate;

    [SerializeField] private MeshRenderer indicatorMeshRenderer;
    [SerializeField] private Material onMaterial;
    [SerializeField] private Material offMaterial;
    [SerializeField] private TextMeshPro textObj;

    [SerializeField] private byte constantValue;

    void Start()
    {
        logicGate = GetComponent<LogicGate>();
    }

    private byte OnOffState(byte[] inputs)
    {
        return constantValue;
    }

    public void UpdateState(int value)
    {
        constantValue = (byte)value;
        string binary = Convert.ToString(constantValue, 2);
        textObj.text = $"{constantValue} ({binary})";
        if (logicGate != null)
        {
            logicGate.OnInputChange(0, constantValue);
        }
    }
}
