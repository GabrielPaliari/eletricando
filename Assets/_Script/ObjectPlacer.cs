using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> placedGameObjects = new();

    public int PlaceObject(PlaceableComponentSO componentData, Vector3 position, RotationDir rotationDir)
    {
        GameObject newObject = Instantiate(componentData.Prefab);

        Vector2Int rotationOffset = RotationUtil.GetRotationOffset(rotationDir, componentData.Size);
        newObject.transform.position = new Vector3(
            position.x + rotationOffset.x,
            position.y,
            position.z + rotationOffset.y);
        newObject.transform.rotation = RotationUtil.GetRotationAngle(rotationDir);

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
