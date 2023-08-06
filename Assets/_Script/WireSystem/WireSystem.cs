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
    }

    private void EndWiring()
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
        setState(WireState.WireModeOn);
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