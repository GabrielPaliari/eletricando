using System;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    public static PlacementSystem instance;

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private Grid grid;

    public static Grid globalGrid;

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

    public GameEvent onBuildComponentSelected, onRemoveComponentSelected, onPlaceComponent, onChangeCursorRemove, onChangeCursorAdd;

    private int selectedComponentId;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gridVisualization.SetActive(false);
        componentsData = new();
        globalGrid = grid;
    }

    public void StartPlacement(int ID)
    {
        onChangeCursorAdd.Raise();
        selectedComponentId = ID;
        onBuildComponentSelected.Raise();
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new PlacementState(ID,
                                           grid,
                                           preview,
                                           database,
                                           componentsData,
                                           objectPlacer);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnRotate += RotateStructure;
        inputManager.OnExit += StopPlacement;
    }

    public void StartRemoving()
    {
        onChangeCursorRemove.Raise();
        onRemoveComponentSelected.Raise();
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new RemovingState(grid, preview, componentsData, objectPlacer);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    public void PreBuildLevelComponents()
    {
        var level = LevelManager.Instance._selectedLevel;
        GridData selectedData = componentsData;
        level.preBuiltComponents.ForEach(component => {
            var selectedComponentIndex = database.objectsData.FindIndex(data => data.ID == component.componentId);
            RotationUtil.currentDir = component.rotation;
            int index = objectPlacer.PlaceObject(database.objectsData[selectedComponentIndex], grid.CellToWorld(component.position), component);
            selectedData.AddObjectAt(component.position,
                database.objectsData[selectedComponentIndex].Size,
                database.objectsData[selectedComponentIndex].ID,
                index,
                true);
        });
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
        SoundFeedback.Instance.PlaySound(SoundType.Click);
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