using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour, IDataPersistence
{
    public static LevelManager Instance;
    
    [SerializeField]
    private GameObject _loadingCanvas;

    public LevelSO _selectedLevel;
    public LevelsListSO _levelsList;
    private GameData _gameData;
    public int lastCompletedLevelStars = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectLevel(LevelSO level) {
        _selectedLevel = level;
    }

    public void LoadSelectedLevel()
    {
        lastCompletedLevelStars = 0;
        StartCoroutine(LoadAsync("PuzzleLevel"));          
    }    

    public void LoadMainMenu()
    {
        StartCoroutine(LoadAsync("MainMenu"));
    }

    public void LoadLevelSelection()
    {
        StartCoroutine(LoadAsync("LevelSelection"));
    }

    IEnumerator LoadAsync(string sceneName)
    {
        //_loadingCanvas.SetActive(true);
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        while (!scene.isDone)
        {
            yield return null;
        }
        //_loadingCanvas.SetActive(false);
    }

    public void CompleteSelectedLevel(int stars)
    {
        lastCompletedLevelStars = stars;
        LoadLevelSelection();
    }

    public void LoadData(GameData data)
    {
        _gameData = data;
    }

    public void SaveData(ref GameData data)
    {
        data.completedLevels = _gameData.completedLevels;
    }
}
