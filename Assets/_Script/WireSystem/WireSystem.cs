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
    LineRenderer lineRenderer;


    public void EnterWireMode()
    {
        setState(WireState.WireModeOn);
        onHighlightOutputs.Raise();
        inputManager.CallOnExit();
    }

    public void ExitWireMode()
    {
        setState(WireState.WireModeOff);
        onDisableHighlights.Raise();
        ResetConnection();
    }

    private void Start()
    {
        setState(WireState.WireModeOff);
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.D) && wireState != WireState.WireModeOff) {
            setState(WireState.DeleteMode);
            Debug.Log("Delete Wire Mode");
        }
        switch (wireState)
        {
            case WireState.WireModeOn:
                if (Input.GetMouseButtonUp(0))
                {
                    GameObject connectorObject = inputManager.GetSelectedWireConector();
                    if (connectorObject != null)
                    {
                    firstComponentGO = connectorObject;
                    
                        StartWiring();
                        onHighlightInputs.Raise();
                        setState(WireState.FirstSelected);
                    }
                }
                break;
            case WireState.FirstSelected:
            case WireState.SecondHovered:
                UpdateWireVisual();
                if (Input.GetMouseButtonUp(0))
                {
                    GameObject secConnectorObject = inputManager.GetSelectedWireConector();
                    if (secConnectorObject != null)
                    {
                        secondComponentGO = secConnectorObject;
                        EndWiring();
                    }
                }
                break;
            case WireState.DeleteMode:
                if (Input.GetMouseButtonUp(0))
                {
                    RaycastHit hit = inputManager.GetHoveredWire();
                    if (hit.collider != null)
                    {                        
                       GameObject.Destroy(hit.collider.gameObject);
                    }
                }
                break;
        }
    }

    private void ResetConnection()
    {
    }

    private void EndWiring()
    {
        lineRenderer.enabled = false;
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
        EnterWireMode();
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

    private void StartWiring()
    {
        lineRenderer.enabled = true;
    }

    private void UpdateWireVisual()
    {
        var startPos = firstComponentGO.transform.position;
        var endPos = inputManager.GetSelectedMapPosition();
        lineRenderer.SetPosition(0, startPos); 
        lineRenderer.SetPosition(1, endPos);
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
    SecondSelected,
    DeleteMode
}