using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private HexGrid _hexGrid;
    [SerializeField] private BuildingScriptable _defaultTilePrefab;
    [SerializeField] private BuildingScriptable _selectedComponent;

    private void Start()
    {
        _hexGrid = new HexGrid(5, _defaultTilePrefab);
    }

    private void Update()
    {
        var _selectedTile = GetPointedTile();

        //_selectedTile.highlight.Off();

        //_selectedTile = GetPointedTile();
        if (_selectedTile != null)
        {
            _selectedTile.highlight.On();            
        }



        if (Input.GetMouseButtonDown(0))
        {
            _selectedTile.Build(_selectedComponent);
        }
    }

    private Tile GetPointedTile()
    {
        var mousePos = Mouse3D.GetMouseWorldPosition();
        mousePos.z = 0f;
        return _hexGrid.GetTile(mousePos);
    }
}
