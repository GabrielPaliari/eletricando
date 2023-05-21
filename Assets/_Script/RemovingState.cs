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
    SoundFeedback soundFeedback;

    public RemovingState(Grid grid,
                         PreviewSystem previewSystem,
                         GridData componentsData,
                         ObjectPlacer objectPlacer,
                         SoundFeedback soundFeedback)
    {
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.componentsData = componentsData;
        this.objectPlacer = objectPlacer;
        this.soundFeedback = soundFeedback;
        previewSystem.StartShowingRemovePreview();
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition, RotationDir rotationDir)
    {
        GridData selectedData = null;
        if (componentsData.CanPlaceObejctAt(gridPosition, Vector2Int.one, rotationDir) == false)
        {
            selectedData = componentsData;
        }

        if (selectedData == null)
        {
            //sound
            soundFeedback.PlaySound(SoundType.wrongPlacement);
        }
        else
        {
            soundFeedback.PlaySound(SoundType.Remove);
            gameObjectIndex = selectedData.GetRepresentationIndex(gridPosition);
            if (gameObjectIndex == -1)
                return;
            selectedData.RemoveObjectAt(gridPosition);
            objectPlacer.RemoveObjectAt(gameObjectIndex);
        }
        Vector3 cellPosition = grid.CellToWorld(gridPosition);
        previewSystem.UpdatePosition(cellPosition, CheckIfSelectionIsValid(gridPosition, rotationDir), rotationDir);
    }

    private bool CheckIfSelectionIsValid(Vector3Int gridPosition, RotationDir dir)
    {
        return !componentsData.CanPlaceObejctAt(gridPosition, Vector2Int.one, dir);
    }

    public void UpdateState(Vector3Int gridPosition, RotationDir rotationDir)
    {
        bool validity = CheckIfSelectionIsValid(gridPosition, rotationDir);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), validity, rotationDir);
    }
}
