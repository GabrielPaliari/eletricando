using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LED : MonoBehaviour, ILogicGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] {
        OnOffState
    };
    public int inputsLength => 1;
    public int outputsLength => 1;

    [SerializeField]
    private MeshRenderer lightMeshRenderer;
    [SerializeField]
    private Material onMaterial;
    [SerializeField]
    private Material offMaterial;

    public byte OnOffState(byte[] inputs)
    {
        if (lightMeshRenderer != null)
        {
            var isOn = inputs[0] > 0;
            lightMeshRenderer.material = isOn ? onMaterial : offMaterial;
        }
        return inputs[0];
    }
}
