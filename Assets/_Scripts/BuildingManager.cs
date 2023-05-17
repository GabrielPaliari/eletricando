using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private SquareGrid _grid;
    [SerializeField] private BuildingScriptable _defaultTilePrefab;
    [SerializeField] private BuildingScriptable _selectedComponent;

    private void Start()
    {
        _grid = new SquareGrid(10, 10, _defaultTilePrefab);
    }

    private void Update()
    {
        var mousePos = Mouse3D.GetMouseWorldPosition();
        var _selectedTile = _grid.GetTile(mousePos);
        Debug.Log(_selectedTile);

        _selectedTile.Highlight();      



        if (Input.GetMouseButtonDown(0))
        {
            _selectedTile.Build(_selectedComponent);
        }
    }
}
