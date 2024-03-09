using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class WireLogic : MonoBehaviour, ILogicGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] {
        BufferInput
    };

    public int inputsLength => 1;
    public int outputsLength => 1;
    public int stateLength => 1;

    [SerializeField] private LineRenderer lineRenderer;
    
    [SerializeField]
    private Material onMaterial;
    [SerializeField]
    private Material offMaterial;

    private byte BufferInput(byte[] inputs)
    {
        UpdateVisuals(inputs[0]);
        return inputs[0];
    }

    private void UpdateVisuals(int state)
    {
        if (lineRenderer == null)
        {
            return;
        }
        lineRenderer.material = state > 0 ? onMaterial : offMaterial;
    }

    public void CreateBranch(List<Vector3> points)
    {
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }    
}
