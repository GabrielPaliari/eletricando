using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class WireLogic : MonoBehaviour, ILogicGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] { };
    public StateFunction[] stateFunctions => new StateFunction[] {
        OnOffState
    };
    public int inputsLength => 1;
    public int outputsLength => 0;
    public int stateLength => 1;

    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private SplineExtrude splineExtrude;
    
    [SerializeField]
    private Material onMaterial;
    [SerializeField]
    private Material offMaterial;

    private void Start()
    {
        meshFilter.mesh = new Mesh();
    }

    private bool OnOffState(BitArray inputs)
    {
        UpdateVisuals(inputs[0]);
        return inputs[0];
    }

    private void Update()
    {
        splineExtrude.Rebuild();
    }

    private void UpdateVisuals(bool isOn)
    {
        meshRenderer.material = isOn ? onMaterial : offMaterial;
        splineExtrude.Rebuild();
    }

    public void CreateBranch(List<Vector3> points)
    {
        var spline = new Spline();
        points.ForEach(p => {        
            spline.Add(new BezierKnot(p), TangentMode.AutoSmooth);
        });
        splineContainer.AddSpline(spline);
        meshRenderer.material = offMaterial;
        splineExtrude.Rebuild();
    }
}
