using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = -1;
    int ID;
    Grid grid;
    PreviewSystem previewSystem;
    ObjectsDatabaseSO database;
    GridData componentsData;
    ObjectPlacer objectPlacer;
    SoundFeedback soundFeedback;

    public PlacementState(int iD,
                          Grid grid,
                          PreviewSystem previewSystem,
                          ObjectsDatabaseSO database,
                          GridData componentsData,
                          ObjectPlacer objectPlacer,
                          SoundFeedback soundFeedback)
    {
        ID = iD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.database = database;
        this.componentsData = componentsData;
        this.objectPlacer = objectPlacer;
        this.soundFeedback = soundFeedback;

        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex > -1)
        {
            previewSystem.StartShowingPlacementPreview(
                database.objectsData[selectedObjectIndex].Prefab,
                database.objectsData[selectedObjectIndex].Size);
        }
        else
            throw new System.Exception($"No object with ID {iD}");

    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        if (!CheckPlacementValidity(gridPosition, selectedObjectIndex))
        {
            soundFeedback.PlaySound(SoundType.wrongPlacement);
            return;
        }
        soundFeedback.PlaySound(SoundType.Place);
        int index = objectPlacer.PlaceObject(database.objectsData[selectedObjectIndex], grid.CellToWorld(gridPosition));

        GridData selectedData = componentsData;
        selectedData.AddObjectAt(gridPosition,
            database.objectsData[selectedObjectIndex].Size,
            database.objectsData[selectedObjectIndex].ID,
            index);

        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = componentsData;

        return selectedData.CanPlaceObejctAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
    }
}

