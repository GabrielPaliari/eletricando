using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LED : MonoBehaviour, ILogicGateSpec
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

    void Start()
    {
        lightMeshRenderer.material = offMaterial;
    }

    private byte OnOffState(byte[] inputs)
    {
        if (lightMeshRenderer != null)
        {
            var isOn = inputs[0] > 0;
            lightMeshRenderer.material = isOn ? onMaterial : offMaterial;
        }
        return inputs[0];
    }
}
