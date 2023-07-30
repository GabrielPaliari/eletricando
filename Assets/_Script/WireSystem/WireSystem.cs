using System;
using UnityEngine;
using UnityEngine.Splines;
using System.Collections;
using System.Collections.Generic;

public class WireSystem : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;
    private GameObject firstComponentGO;
    private GameObject secondComponentGO;
    
    [SerializeField]
    private SplineContainer splineContainer;
    [SerializeField]
    private SplineExtrude splineExtrude;

    [Header("Line Renderer Settings")]
    [SerializeField] private float startWidth = 0.1f;
    [SerializeField] private float endWidth = 0.1f;
    [SerializeField] private Color lineColor = Color.black;

    private WireState wireState;
    
    private GameObject wireObject;
    private LineRenderer lineRenderer;
    
    public void EnterWireMode()
    {
        setState(WireState.WireModeOn);
        inputManager.CallOnExit();
    }

    public void ExitWireMode()
    {
        setState(WireState.WireModeOff);
        ResetConnection();
    }

    private void Start()
    {
        setState(WireState.WireModeOff);
    }

    private void Update()
    {
        switch (wireState)
        {
            case WireState.WireModeOn:
                GameObject connectorObject = inputManager.GetSelectedWireConector();
                if (connectorObject != null)
                {
                    firstComponentGO = connectorObject;
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartWiring();
                        setState(WireState.FirstSelected);
                    }
                }
                break;
            case WireState.FirstSelected:
            case WireState.SecondHovered:
                UpdateWireVisual();
                GameObject secConnectorObject = inputManager.GetSelectedWireConector();
                if (secConnectorObject != null)
                {
                    secondComponentGO = secConnectorObject;
                    if (Input.GetMouseButtonDown(0))
                    {
                        EndWiring();
                    }
                }
                break;
        }
    }

    private void ResetConnection()
    {
        Destroy(wireObject);
        wireObject = null;
    }

    private void EndWiring()
    {
        var firstLogicGate = firstComponentGO.GetComponentInParent<LogicGate>();
        var firstConnector = firstComponentGO.GetComponent<WireConector>();

        var secondLogicGate = secondComponentGO.GetComponentInParent<LogicGate>();
        var secondConnector = secondComponentGO.GetComponent<WireConector>();

        LogicCircuitSystem.Instance.RegisterOutputListener(firstLogicGate, firstConnector.index, secondLogicGate, secondConnector.index);
        //lineRenderer.SetPosition(1, secondWireConector.transform.position);
        splineContainer.AddSpline(CreateSplineFromPoints());
        splineExtrude.Rebuild();
        wireObject = null;
        setState(WireState.WireModeOn);
    }

    private Spline CreateSplineFromPoints()
    {
        Vector3 posA = firstComponentGO.transform.position;
        Vector3 posB = secondComponentGO.transform.position;
        
        Vector3 posM = (posA + posB) / 2;
        
        posM.y = posM.y * 1/ (float)Math.Log(Vector3.Distance(posA, posB)/2 + 3);

        var spline = new Spline();
        spline.Add(new BezierKnot(posA), TangentMode.AutoSmooth);
        spline.Add(new BezierKnot(posM), TangentMode.AutoSmooth);
        spline.Add(new BezierKnot(posB), TangentMode.AutoSmooth);
        return spline;
    }

    private void StartWiring()
    {
        wireObject = new GameObject("Wire");

        //lineRenderer.positionCount = 2;
        //lineRenderer.startWidth = startWidth;
        //lineRenderer.endWidth = endWidth;
        //lineRenderer.startColor = lineColor;
        //lineRenderer.endColor = lineColor;

        //lineRenderer.SetPosition(0, firstWireConector.transform.position);
    }

    private void UpdateWireVisual()
    {
        var pos = inputManager.GetSelectedMapPosition();
        //lineRenderer.SetPosition(1, pos);
    }

    private void setState(WireState state)
    {
        wireState = state;
    }
}
public enum WireState
{
    WireModeOff,
    WireModeOn,
    FirstHovered,
    FirstSelected,
    SecondHovered,
    SecondSelected
}