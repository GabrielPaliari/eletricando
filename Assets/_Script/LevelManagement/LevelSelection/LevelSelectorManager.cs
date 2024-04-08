using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelSelectorManager : MonoBehaviour, IDataPersistence
{
    public static LevelSelectorManager instance;
    [SerializeField] private LevelSelectionCameraController _cameraController;
    [SerializeField] Transform _levelButton_pf;
    private List<LevelSelectorButton> _levelButtons = new();
    [SerializeField] float _gap = 10f;
    private GameData _gameData;
    private float _xOffset = 0f;    
    private bool _previousLevelComplete = true;

    [SerializeField] LineRenderer _lineRenderer;
    private List<LineRenderer> lineRenderers = new();
    [SerializeField] private Color _lineEnabledColor;
    [SerializeField] private Color _lineDisabledColor;
    
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
            var lineColor = levelComplete ? _lineEnabledColor : _lineDisabledColor;

            var line = Instantiate(_lineRenderer);
            var firstX = levelIndex * _gap;
            var secondX = firstX + _gap;

            line.positionCount = 2;
            line.startColor = lineColor;
            line.endColor = lineColor;
            line.SetPosition(0, new Vector3(firstX, 0, 10));            
            line.SetPosition(1, new Vector3(secondX, 0, 10));
            lineRenderers.Add(line);
        } 
    }

    public void SelectLevel(LevelSO level)
    {
        LevelManager.Instance.SelectLevel(level);
    }

    public void PlayLevel()
    {
        LevelManager.Instance.LoadSelectedLevel();
    }

    public void LoadData(GameData data)
    {
        _gameData = data;
        InitializeLevelButtons();
        if (!CompletedLevelRecently())
        {
            SelectLastCompletedLevel();
        }
    }

    public void SaveData(ref GameData data)
    {
        data.completedLevels = _gameData.completedLevels;
    }

    private void SelectLastCompletedLevel()
    {
        LevelSelectorButton lastCompletedLevelBtn;
        var lastCompletedId = _gameData.completedLevels.Count;
        if (lastCompletedId < LevelManager.Instance._levelsList.levelData.Count)
        {
            lastCompletedLevelBtn = _levelButtons[lastCompletedId];
        } else
        {
            lastCompletedLevelBtn = _levelButtons[lastCompletedId - 1];
        }        
        _cameraController.SelectLevelButton(lastCompletedLevelBtn.gameObject);
    }

    private bool CompletedLevelRecently()
    {
        var currentLevelStars = LevelManager.Instance.lastCompletedLevelStars;
        if (currentLevelStars > 0)
        {
            var currentLevelId = LevelManager.Instance._selectedLevel.id;
            var currentLevelIndex = currentLevelId - 1;
            AnimateCurrentLevelStars(currentLevelIndex, currentLevelStars);
            SaveLevelComplete(currentLevelId, currentLevelStars);
            return true;
        } else
        {
            return false;
        }
    }

    private void AnimateCurrentLevelStars(int currentLevelIndex, int stars)
    {
        LevelSelectorButton levelButton = _levelButtons[currentLevelIndex];
        var levelAreadyCompleted = _gameData.completedLevels.Count > currentLevelIndex;
        var cameraSequence = _cameraController.SelectLevelButton(levelButton.gameObject);
        cameraSequence.OnComplete(() => { 
            var levelStarsSequence = levelButton.CompleteLevel(stars);
            levelStarsSequence.OnComplete(() => { 
                if (!levelAreadyCompleted)
                {
                    UnlockNextLevel(currentLevelIndex + 1);
                }
            });
        });

    }

    private void SaveLevelComplete(int levelId, int stars)
    {
        CompletedLevelInfo completedLevel = _gameData.completedLevels.Find((levelSO) => levelSO.id == levelId);
        if (completedLevel != null)
        {
            if (stars > completedLevel.starsNumber)
            {
                completedLevel.starsNumber = stars;
            }
        } else
        {
            _gameData.completedLevels.Add(new ()
            {
                id = levelId,
                starsNumber = stars,
            });
        }
        DataPersistenceManager.instance.SaveGame();
    }

    private void UnlockNextLevel(int nextLevelIndex)
    {
        if (nextLevelIndex < _levelButtons.Count)
        {
            LevelSelectorButton levelButton = _levelButtons[nextLevelIndex];
            var cameraSeq = _cameraController.SelectLevelButton(levelButton.gameObject);
            AnimateLineRenderer(nextLevelIndex);
            cameraSeq.OnComplete(() => {                
                levelButton.ToggleLevelEnabledAnimated();
            });
        }

    }

    private void AnimateLineRenderer(int nextLevelIndex)
    {
        var line = lineRenderers[nextLevelIndex - 1];
        Vector3 startPosition = line.GetPosition(0);
        Vector3 endPosition = line.GetPosition(1);

        var newLine = Instantiate(_lineRenderer);
        newLine.startColor = _lineEnabledColor;
        newLine.endColor = _lineEnabledColor;
        newLine.positionCount = 2;

        var newStartPos = new Vector3(startPosition.x, startPosition.y, startPosition.z - 0.1f);
        newLine.SetPosition(0, newStartPos);
        newLine.SetPosition(1, newStartPos);
        Vector3 targetPosition = new Vector3(startPosition.x + 10f, newStartPos.y, newStartPos.z);

        DOTween.To(() => newLine.GetPosition(1),
                    (x) => newLine.SetPosition(1, x),
                    targetPosition,
                    1f)
               .Play();
    }
}