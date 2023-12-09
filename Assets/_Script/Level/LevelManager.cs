using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    [SerializeField]
    private GameObject levelButtonPrefab;
    
    [SerializeField]
    private GameObject levelsGridParent;
    
    [SerializeField]
    private LevelsListSO levelsList;

    private LevelSO m_selectedLevel;

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

    void Start()
    {
        if (levelsGridParent != null) { 
            levelsList.levelData.ForEach((level) =>
            {
                var levelButton = Instantiate(levelButtonPrefab);
                levelButton.transform.SetParent(levelsGridParent.transform, false);
                levelButton.GetComponent<LevelButton>().level = level;
            });
        }
    }

    public void LoadLevel(LevelSO level)
    {
        m_selectedLevel = level;
        SceneManager.LoadScene("CircuitBuild");
    } 
}
