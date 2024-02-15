using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    [SerializeField]
    private GameObject _loadingCanvas;

    [SerializeField]
    private Image _progressBar;

    public LevelSO _selectedLevel;
    public LevelsListSO _levelsList;

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
    
    public void LoadNextLevel()
    {
        var nextLevelId = _selectedLevel.id + 1;
        var nextLevel = _levelsList.levelData.Find((level) => level.id == nextLevelId);
        if (nextLevel == null)
        {
            Debug.Log("ZEROOOOOU");
        } else
        {
            _selectedLevel = nextLevel;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void LoadLevel(LevelSO level)
    {
        _selectedLevel = level;        
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
            _progressBar.fillAmount = scene.progress;
            yield return null;
        }
        _loadingCanvas.SetActive(false);
    }
}
