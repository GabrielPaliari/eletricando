using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingState : IBuildingState
{
    private int gameObjectIndex = -1;
    Grid grid;
    PreviewSystem previewSystem;
    GridData componentsData;
    ObjectPlacer objectPlacer;

    public RemovingState(Grid grid,
                         PreviewSystem previewSystem,
                         GridData componentsData,
                         ObjectPlacer objectPlacer)
    {
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.componentsData = componentsData;
        this.objectPlacer = objectPlacer;
        previewSystem.StartShowingRemovePreview();
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        GridData selectedData = null;
        if (CheckIfSelectionIsValid(gridPosition))
        {
            selectedData = componentsData;
        }

        if (selectedData == null)
        {
            SoundFeedback.Instance.PlaySound(SoundType.WrongPlacement);
        }
        else
        {
            SoundFeedback.Instance.PlaySound(SoundType.Remove);
            gameObjectIndex = selectedData.GetRepresentationIndex(gridPosition);
            if (gameObjectIndex == -1)
                return;
            selectedData.RemoveObjectAt(gridPosition);
            objectPlacer.RemoveObjectAt(gameObjectIndex);
        }
        Vector3 cellPosition = grid.CellToWorld(gridPosition);
        previewSystem.UpdatePosition(cellPosition, CheckIfSelectionIsValid(gridPosition));
    }

    private bool CheckIfSelectionIsValid(Vector3Int gridPosition)
    {
        return (!componentsData.CanPlaceObejctAt(gridPosition, Vector2Int.one)
            && !componentsData.HasIndestructibleObject(gridPosition));
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool validity = CheckIfSelectionIsValid(gridPosition);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), validity);
    }
}
