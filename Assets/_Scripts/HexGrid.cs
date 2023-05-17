using System.Collections.Generic;
using UnityEngine;



public class HexGrid : Object
{
    private BuildingScriptable _defaultBuildingData; // Prefab for the hexagons
    private int _gridRadius = 10; // Number of hexagons in each row/column
    private float _hexSize = 0.5f; // Size of each hexagon

    public HexGrid(
        int gridRadius,
        BuildingScriptable defaultBuildingData = default(BuildingScriptable)
    )
    {
        _gridRadius = gridRadius;
        _defaultBuildingData = defaultBuildingData;
        GenerateGrid();
    }

    private Dictionary<Vector2Int, Tile> _tiles = new Dictionary<Vector2Int, Tile>(); // Dictionary to store the cells

    private void GenerateGrid()
    {
        for (int x = -_gridRadius; x <= _gridRadius; x++)
        {
            for (int y = -_gridRadius; y <= _gridRadius; y++)
            {
                int z = -x - y;

                if (Mathf.Abs(x) <= _gridRadius && Mathf.Abs(y) <= _gridRadius && Mathf.Abs(z) <= _gridRadius)
                {
                    Vector3 cubeCoordinates = new Vector3(x, y, z);
                    Vector2Int axialCoordinates = CubeToAxial(cubeCoordinates);
                    Vector3 worldCoordinates = AxialToWorld(axialCoordinates);

                    GameObject hexGameObject = Instantiate(_defaultBuildingData._initialPrefab, worldCoordinates,Quaternion.identity);
                    Tile tile = new Tile(worldCoordinates, axialCoordinates, _defaultBuildingData, hexGameObject);
                    _tiles[axialCoordinates] = tile;
                }
            }
        }
    }

    private Vector2Int CubeToAxial(Vector3 cube)
    {
        int q = (int)cube.x;
        int r = (int)cube.z;

        return new Vector2Int(q, r);
    }

    private Vector3 AxialToWorld(Vector2Int axial)
    {
        float x = _hexSize * (Mathf.Sqrt(3.0f) * axial.x + Mathf.Sqrt(3.0f) / 2.0f * axial.y);
        float y = _hexSize * (3.0f / 2.0f * axial.y);

        return new Vector3(x, y, 0);
    }

    private Vector2Int WorldToAxial(Vector3 worldPosition)
    {
        float q = (2.0f / 3.0f * worldPosition.y) / _hexSize;
        float r = (-1.0f / 3.0f * worldPosition.y + Mathf.Sqrt(3.0f) / 3.0f * worldPosition.x) / _hexSize;

        int rRounded = Mathf.RoundToInt(r);
        int qRounded = Mathf.RoundToInt(q);

        return new Vector2Int(rRounded, qRounded);
    }

    public Tile GetTile(Vector3 worldPosition)
    {
        return GetTile(WorldToAxial(worldPosition));
    }

    public Tile GetTile(Vector2Int axial)
    {
        try
        {
            return _tiles[axial];
        }
        catch
        {
            return default(Tile);
        }
    }

    public void SetTile(Vector3 worldPosition, BuildingScriptable data)
    {
        SetTile(WorldToAxial(worldPosition), data);
    }

    public void SetTile(Vector2Int axial, BuildingScriptable data)
    {
        if (_tiles[axial] != null) _tiles[axial].buildingData = data;
    }
}