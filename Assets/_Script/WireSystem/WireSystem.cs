using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SocialPlatforms;
using static UnityEditor.PlayerSettings;

public class WireSystem : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    public event Action OnBeginWiring, OnEndWiring;
    private WireConector firstWireConector;
    private WireConector secondWireConector;
    
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

    private void Update()
    {
        switch (wireState)
        {
            case WireState.WireModeOn:
                GameObject connectorObject = inputManager.GetSelectedWireConector();
                if (connectorObject != null)
                {
                    firstWireConector = connectorObject.GetComponent<WireConector>();
                    firstWireConector.ApplyFeedbackToIndicator(true);
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
                    secondWireConector = secConnectorObject.GetComponent<WireConector>();
                    secondWireConector.ApplyFeedbackToIndicator(true);
                    if (Input.GetMouseButtonDown(0))
                    {
                        EndWiring();
                    }
                }
                break;
            case WireState.SecondSelected:

                break;
        }
    }

    private void EndWiring()
    {
        lineRenderer.SetPosition(1, secondWireConector.transform.position);
        setState(WireState.SecondSelected);
    }

    private void StartWiring()
    {
        wireObject = new GameObject("Wire");
        lineRenderer = wireObject.AddComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        lineRenderer.SetPosition(0, firstWireConector.transform.position);
    }

    private void UpdateWireVisual()
    {
        var pos = inputManager.GetSelectedMapPosition();
        lineRenderer.SetPosition(1, pos);
    }

    private void setState(WireState state)
    {
        wireState = state;
    }
}
public enum WireState
{
    WireModeOn,
    FirstHovered,
    FirstSelected,
    SecondHovered,
    SecondSelected
}