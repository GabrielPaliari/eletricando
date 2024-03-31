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
        StartCoroutine(LoadAsync("CircuitBuild"));          
    }    

    public void LoadMainMenu()
    {
        _selectedLevel = null;
        StartCoroutine(LoadAsync("MainMenu"));
    }

    IEnumerator LoadAsync(string sceneName)
    {
        _loadingCanvas.SetActive(true);
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        while (!scene.isDone)
        {
            yield return null;
        }
        _loadingCanvas.SetActive(false);
    }

    public void CompleteSelectedLevel(int stars)
    {
        var id = _selectedLevel.id;
        _gameData.completedLevels.RemoveAll((level) => level.id == id);
        
        _gameData.completedLevels.Add(new()
        {
            id = id,
            starsNumber = stars
        });
        
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
