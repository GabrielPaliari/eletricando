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
    private GameObject wirePrefab;    
    private WireState wireState;
    public GameEvent onHighlightInputs;
    public GameEvent onHighlightOutputs;
    public GameEvent onDisableHighlights;
    public GameEvent onRemoveLogicGate;
    LineRenderer lineRenderer;

    private void setState(WireState state)
    {
        wireState = state;
        UpdateUserFeedback();
    }

    private void UpdateUserFeedback()
    {
        switch (wireState)
        {
            case WireState.WireModeOn:
                onHighlightOutputs.Raise();
                break;
            case WireState.FirstSelected:
                onHighlightInputs.Raise();
                break;
            case WireState.WireModeOff:
            case WireState.DeleteMode:
                DisablePreview();
                onDisableHighlights.Raise();
                break;
        }
    }

    private void Start()
    {
        setState(WireState.WireModeOff);
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R)) {
            if (wireState == WireState.DeleteMode)
            {
                setState(WireState.WireModeOn);
            } else if (wireState != WireState.WireModeOff)
            {
                setState(WireState.DeleteMode);
            }
        }
        switch (wireState)
        {
            case WireState.WireModeOn:
                if (Input.GetMouseButtonDown(0))
                {
                    StartWiring();
                }
                break;
            case WireState.FirstSelected:
                UpdatePreviewVisual();
                if (Input.GetMouseButtonUp(0))
                {
                    EndWiring();
                }
                break;
            case WireState.DeleteMode:
                if (Input.GetMouseButtonUp(0))
                {
                    DeleteWire();
                }
                break;
        }
    }

    public void EnterWireMode()
    {
        setState(WireState.WireModeOn);
        inputManager.CallOnExit();
    }

    public void ExitWireMode()
    {
        setState(WireState.WireModeOff);
    }

    private void StartWiring()
    {
        GameObject connectorObject = inputManager.GetSelectedWireConector();
        if (connectorObject != null)
        {
            lineRenderer.enabled = true;
            firstComponentGO = connectorObject;
            setState(WireState.FirstSelected);
        }
    }

    private void UpdatePreviewVisual()
    {
        var startPos = firstComponentGO.transform.position;
        var endPos = inputManager.GetSelectedMapPosition();
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    private void EndWiring()
    {
        GameObject secConnectorObject = inputManager.GetSelectedWireConector();
        if (secConnectorObject != null)
        {
            secondComponentGO = secConnectorObject;           
            CreateWirePrefab();
        }
        DisablePreview();
        EnterWireMode();
    }

    private void DisablePreview()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }

    private void CreateWirePrefab()
    {
        var firstLogicGate = firstComponentGO.GetComponentInParent<LogicGate>();
        var firstConnector = firstComponentGO.GetComponent<WireConector>();

        var secondLogicGate = secondComponentGO.GetComponentInParent<LogicGate>();
        var secondConnector = secondComponentGO.GetComponent<WireConector>();

        var wireObject = Instantiate(wirePrefab);
        var wireLogic = wireObject.GetComponent<WireLogic>();
        LogicGate wire = wireObject.GetComponent<LogicGate>();
        wire.Initialize();

        LogicCircuitSystem.Instance.RegisterOutputListener(firstLogicGate, firstConnector.index, secondLogicGate, secondConnector.index, wire);
        wireLogic.CreateBranch(GetWirePositions());
    }

    private List<Vector3> GetWirePositions()
    {
        var pointList = new List<Vector3>();
        Vector3 posA = firstComponentGO.transform.position;
        Vector3 posB = secondComponentGO.transform.position;
        pointList.Add(posA);
        pointList.Add(posB);

        return pointList;
    }

    private void DeleteWire()
    {
        RaycastHit hit = inputManager.GetHoveredWire();
        if (hit.collider != null)
        {
            Destroy(hit.collider.gameObject);
        }
    }
}
public enum WireState
{
    WireModeOff,
    WireModeOn,
    FirstHovered,
    FirstSelected,
    DeleteMode
}