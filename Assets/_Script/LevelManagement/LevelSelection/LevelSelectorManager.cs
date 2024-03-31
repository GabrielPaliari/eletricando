using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorManager : MonoBehaviour, IDataPersistence
{
    public static LevelSelectorManager instance;
    [SerializeField] private PanZoom _cameraController;
    [SerializeField] Transform _levelButton_pf;
    private List<LevelSelectorButton> _levelButtons = new();
    [SerializeField] float _gap = 10f;
    [SerializeField] LineRenderer _lineRendererEnabled;
    [SerializeField] LineRenderer _lineRendererDisabled;
    private GameData _gameData;
    private float _xOffset = 0f;    
    private bool _previousLevelComplete = true;

    private void Awake()
    {
        instance = this;
    }

    private void InitializeLevelButtons()
    {
        int levelIndex = 0;
        var levels = LevelManager.Instance._levelsList.levelData;
        var levelsCount = levels.Count;
        levels.ForEach(level => {
            var levelButtonObj = Instantiate(_levelButton_pf);
            levelButtonObj.position = new Vector3(levelButtonObj.position.x + _xOffset, levelButtonObj.position.y, 0);
            var levelButton = levelButtonObj.GetComponent<LevelSelectorButton>();
            _levelButtons.Add(levelButton);
            var completedLevelData = _gameData.completedLevels.Find((l) => l.id == level.id);
            levelButton.InitializeButton(_previousLevelComplete, level, completedLevelData);

            _previousLevelComplete = completedLevelData != null;
            CreateConnectionToNext(levelIndex, levelsCount, _previousLevelComplete);

            levelIndex++;
            _xOffset += _gap;
        });
    }

    private void CreateConnectionToNext(int levelIndex, int levelCount, bool levelComplete)
    {
        if (levelIndex < levelCount - 1)
        {
            var linePrefab = levelComplete ? _lineRendererEnabled : _lineRendererDisabled;

            var line = Instantiate(linePrefab);
            var firstX = levelIndex * _gap;
            var secondX = firstX + _gap;

            line.positionCount = 2;
            line.SetPosition(0, new Vector3(firstX, 0, 10));            
            line.SetPosition(1, new Vector3(secondX, 0, 10));
        } 
    }

    public void SelectLevel(LevelSO level)
    {
        CentralizeLevelHexInCamera();
        LevelManager.Instance.SelectLevel(level);
        _levelButtons.ForEach(button => button.DesselectButton());
    }

    public void PlayLevel()
    {
        LevelManager.Instance.LoadSelectedLevel();
    }

    private void CentralizeLevelHexInCamera()
    {
        //_cameraController.goToPosition();
    }

    public void LoadData(GameData data)
    {
        _gameData = data;
        InitializeLevelButtons();
    }

    public void SaveData(ref GameData data)
    {
    }
}