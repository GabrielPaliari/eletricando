using System.Collections.Generic;
using UnityEngine;



public class SquareGrid : Object
{
    private BuildingScriptable _defaultBuildingData; // Prefab for the square
    private int _width = 10; // Number of cells in each row/column
    private int _height = 10; // Number of cells in each row/column
    private float _cellSize = 0.5f; // Size of each cell

    public SquareGrid(
        int width, int height,
        BuildingScriptable defaultBuildingData = default(BuildingScriptable)
    )
    {
        _width = width;
        _height = height;

        _defaultBuildingData = defaultBuildingData;
        GenerateGrid();
    }

    private Dictionary<Vector2Int, Tile> _tiles = new Dictionary<Vector2Int, Tile>(); // Dictionary to store the hexagons

    private void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                    Vector2Int cellCoordinates = new Vector2Int(x, y);
                    Vector3 worldCoordinates = GridToWorld(cellCoordinates);

                    GameObject hexGameObject = Instantiate(_defaultBuildingData._initialPrefab, worldCoordinates, Quaternion.identity);
                    Tile tile = new Tile(worldCoordinates, cellCoordinates, _defaultBuildingData, hexGameObject);
                    _tiles[cellCoordinates] = tile;
            }
        }
    }

    private Vector3 GridToWorld(Vector2Int cellCoord)
    {
        float x = _cellSize * cellCoord.x;
        float y = _cellSize * cellCoord.y;

        return new Vector3(x, y, 0);
    }

    private Vector2Int WorldToGrid(Vector3 worldPosition)
    {
        float x = worldPosition.x / _cellSize;
        float y = worldPosition.y / _cellSize;

        int xRounded = Mathf.RoundToInt(x);
        int yRounded = Mathf.RoundToInt(y);

        return new Vector2Int(xRounded, yRounded);
    }

    public Tile GetTile(Vector3 worldPosition)
    {
        return GetTile(WorldToGrid(worldPosition));
    }

    public Tile GetTile(Vector2Int cellCoord)
    {
        try
        {
            return _tiles[cellCoord];
        }
        catch
        {
            return default(Tile);
        }
    }

    public void SetTile(Vector3 worldPosition, BuildingScriptable data)
    {
        SetTile(WorldToGrid(worldPosition), data);
    }

    public void SetTile(Vector2Int cellCoord, BuildingScriptable data)
    {
        if (_tiles[cellCoord] != null) _tiles[cellCoord].buildingData = data;
    }
}