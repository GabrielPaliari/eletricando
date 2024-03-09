using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> placedGameObjects = new();
    public GameEvent onRemoveLogicGate;

    public int PlaceObject(PlaceableComponentSO componentData, Vector3Int cellPos, BuildedComponentSO buildedCompSpec = null)
    {
        GameObject newObject = Instantiate(componentData.Prefab);

        LogicGate logicGate = newObject.GetComponent<LogicGate>();
        if(logicGate != null) {
            var rotationDir = RotationUtil.currentDir;
            var compId = componentData.ID;
            var buildedId = buildedCompSpec != null ? buildedCompSpec.buidedId : 0;
            var initialState = buildedCompSpec != null ? buildedCompSpec.initialState : 0;
            logicGate.Initialize(compId, cellPos, rotationDir, buildedId, initialState); 
        }

        ISignalSeqGateSpec signalGate = newObject.GetComponent<ISignalSeqGateSpec>();
        if (signalGate != null) { signalGate.Initialize(logicGate.id, buildedCompSpec); }

        Vector2Int rotationOffset = RotationUtil.GetRotationOffset(componentData.Size);
        Vector3 pos = PlacementSystem.globalGrid.CellToWorld(cellPos);
        newObject.transform.position = new Vector3(
            pos.x + rotationOffset.x,
            pos.y,
            pos.z + rotationOffset.y);
        newObject.transform.rotation = RotationUtil.GetRotationAngle();

        placedGameObjects.Add(newObject);
        return placedGameObjects.Count - 1;
    }

    internal void RemoveObjectAt(int gameObjectIndex)
    {
        if (placedGameObjects.Count <= gameObjectIndex
            || placedGameObjects[gameObjectIndex] == null)
            return;
        Destroy(placedGameObjects[gameObjectIndex]);
        placedGameObjects[gameObjectIndex] = null;
    }
}
