using System;
using UnityEngine;
using UnityEngine.Splines;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class WireSystem : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;
    private GameObject firstComponentGO;
    private GameObject secondComponentGO;

    [SerializeField]
    private GameObject wirePrefab;    
    private WireState wireState;
    public GameEvent
        onHighlightInputs,
        onHighlightOutputs,
        onDisableHighlights,
        onRemoveLogicGate,
        onChangeCursorAdd,
        onChangeCursorRemove,
        onChangeCursorRemoveHighlight,
        onChangeCursorDefault;
    LineRenderer lineRenderer;

    private List<Vector3> wireNodes = new();


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
                onChangeCursorAdd.Raise();
                onHighlightOutputs.Raise();
                break;
            case WireState.FirstSelected:
                onHighlightInputs.Raise();
                break;
            case WireState.WireModeOff:
                onChangeCursorDefault.Raise();
                DisablePreview();
                onDisableHighlights.Raise();
                break;
            case WireState.DeleteMode:
                onChangeCursorRemove.Raise();
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
                if (Input.GetMouseButtonUp(0))
                {
                    StartWiring();
                }
                break;
            case WireState.FirstSelected:
                UpdatePreviewLastNode();
                if (Input.GetMouseButtonUp(0))
                {
                    CreateNextNode();
                }
                break;
            case WireState.DeleteMode:
                RaycastHit hit = inputManager.GetHoveredWire();
                if (hit.collider != null)
                {
                    onChangeCursorRemoveHighlight.Raise();
                    if (Input.GetMouseButtonUp(0))
                    {
                        DeleteWire(hit);
                    }
                } else
                {
                    onChangeCursorRemove.Raise();
                }
                break;
        }
    }

    public void EnterWireMode()
    {
        setState(WireState.WireModeOn);
        inputManager.CallOnExit();
    }

    public void EnterWireDeleteMode()
    {
        setState(WireState.DeleteMode);
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
            lineRenderer.positionCount = 1;
            wireNodes = new()
            {
                SetDefaultHeight(connectorObject.transform.position)
            };
            UpdatePreviewNodes();
            lineRenderer.enabled = true;
            firstComponentGO = connectorObject;
            setState(WireState.FirstSelected);
        }
    }

    private Vector3 SetDefaultHeight(Vector3 pos)
    {
        pos.y = .1f;
        return pos;
    }
    private void UpdatePreviewNodes()
    {
        lineRenderer.SetPositions(wireNodes.ToArray());
    }

    private void UpdatePreviewLastNode()
    {
        var pointedCell = (Vector3)inputManager.GetHoveredCellCenter();
        lineRenderer.positionCount = wireNodes.Count + 1;
        lineRenderer.SetPosition(wireNodes.Count, SetDefaultHeight(pointedCell));
    }

    private void CreateNextNode()
    {
        GameObject lastConector = inputManager.GetSelectedWireConector();
        if (lastConector != null)
        {
            EndWiring(lastConector);
        } else
        {
            var nextPos = inputManager.GetHoveredCellCenter();
            if (nextPos != null)
            {
                wireNodes.Add(SetDefaultHeight((Vector3) nextPos));
                UpdatePreviewNodes();
            }
        }
    }

    private void EndWiring(GameObject lastConector)
    {
        secondComponentGO = lastConector;
        wireNodes.Add(SetDefaultHeight(lastConector.transform.position));
        CreateWirePrefab();        
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
        wireLogic.CreateBranch(wireNodes);
    }

    private void DeleteWire(RaycastHit hit)
    {                
       Destroy(hit.collider.gameObject);
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