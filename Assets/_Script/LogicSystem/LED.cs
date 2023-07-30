using System.Collections;
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

    private bool OnOffState(BitArray inputs)
    {
        if (inputs[0])
        {
            Debug.Log("LED ligado");
            lightMeshRenderer.material = onMaterial;
        }
        else
        {
            Debug.Log("LED desligado");
            lightMeshRenderer.material = offMaterial;
        }
        return inputs[0];
    }
}
