using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;

    private Vector3 lastPosition;

    [SerializeField]
    private LayerMask placementLayermask;

    [SerializeField]
    private LayerMask wireConectorsLayermask;
    [SerializeField]
    private LayerMask wireLayerMask;

    public event Action OnClicked, OnExit, OnRotate;

    [Header("Events")]
    public GameEvent onEsc;

    public void CallOnExit()
    {
        OnExit?.Invoke();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClicked?.Invoke();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnExit?.Invoke();
            onEsc.Raise();
        }
        if (Input.GetKeyDown(KeyCode.R))
            OnRotate?.Invoke();
    }

    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();

    // TODO: Refactorar essas 3 funções seguintes encapsulando a lógica em uma só
    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200, placementLayermask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }

    public GameObject GetSelectedWireConector()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, wireConectorsLayermask))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    public RaycastHit GetHoveredWire()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, wireLayerMask))
        {
            return hit;
        }
        return hit;

    }
}
