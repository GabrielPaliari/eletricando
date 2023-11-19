using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static PlacementSystem;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    public ObjectsDatabaseSO database;

    [SerializeField]
    private GameObject gridVisualization;

    private GridData componentsData;

    [SerializeField]
    private PreviewSystem preview;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    [SerializeField]
    private ObjectPlacer objectPlacer;

    IBuildingState buildingState;

    [SerializeField]
    private SoundFeedback soundFeedback;

    [Header("Events")]
    public GameEvent onBuildComponentSelected;
    public GameEvent onRemoveComponentSelected;
    public GameEvent onPlaceComponent;

    private int selectedComponentId;

    private void Start()
    {
        gridVisualization.SetActive(false);
        componentsData = new();
    }

    public void StartPlacement(int ID)
    {
        selectedComponentId = ID;
        onBuildComponentSelected.Raise();
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new PlacementState(ID,
                                           grid,
                                           preview,
                                           database,
                                           componentsData,
                                           objectPlacer,
                                           soundFeedback);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnRotate += RotateStructure;
        inputManager.OnExit += StopPlacement;
    }

    public void StartRemoving()
    {
        onRemoveComponentSelected.Raise();
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new RemovingState(grid, preview, componentsData, objectPlacer, soundFeedback);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }
        Vector3Int gridPosition = GetPointedGridPosition();
        buildingState.OnAction(gridPosition);

        var selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == selectedComponentId);
        onPlaceComponent.Raise(new ElectricData("teste"));
    }

    private Vector3Int GetPointedGridPosition()
    {
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        return gridPosition;
    }

    private void RotateStructure()
    {
        RotationUtil.UpdateRotation();
        buildingState.UpdateState(GetPointedGridPosition());
    }

    private void StopPlacement()
    {
        soundFeedback.PlaySound(SoundType.Click);
        if (buildingState == null)
            return;
        gridVisualization.SetActive(false);
        buildingState.EndState();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnRotate -= RotateStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
        buildingState = null;
    }

    private void Update()
    {
        if (buildingState == null)
            return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        if (lastDetectedPosition != gridPosition)
        {
            buildingState.UpdateState(gridPosition);
            lastDetectedPosition = gridPosition;
        }

    }
}