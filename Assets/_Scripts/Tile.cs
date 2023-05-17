using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : Object
{
    public BuildingScriptable buildingData;
    public Vector3 position { get; private set; }
    public Vector2Int axial { get; private set; }
    private GameObject _sceneObject;
    public HighlightHex highlight;

    public Tile(Vector3 _position, Vector2Int _axial, BuildingScriptable _buildingData, GameObject gameObject)
    {
        position = _position;
        axial = _axial;
        buildingData = _buildingData;
        _sceneObject = gameObject;
    }

    public void Build(BuildingScriptable building)
    {
        buildingData = building;
        Destroy(_sceneObject);
        _sceneObject = Instantiate(buildingData._initialPrefab, position, Quaternion.identity);
    }    
    
    public void Highlight()
    {
        Debug.Log("teste");
        HighlightHex highlight = _sceneObject.GetComponent<HighlightHex>();
        highlight.On();
    }
}