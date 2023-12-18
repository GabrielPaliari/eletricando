using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> placedGameObjects = new();
    public GameEvent onRemoveLogicGate;

    public int PlaceObject(PlaceableComponentSO componentData, Vector3 position, List<int> signalSequence)
    {
        GameObject newObject = Instantiate(componentData.Prefab);

        LogicGate logicGate = newObject.GetComponent<LogicGate>();
        if(logicGate != null ) { logicGate.Initialize(); }

        ISignalSeqGateSpec signalGate = newObject.GetComponent<ISignalSeqGateSpec>();
        if (signalGate != null) { signalGate.Initialize(logicGate.id, signalSequence); }

        Vector2Int rotationOffset = RotationUtil.GetRotationOffset(componentData.Size);
        newObject.transform.position = new Vector3(
            position.x + rotationOffset.x,
            position.y,
            position.z + rotationOffset.y);
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
