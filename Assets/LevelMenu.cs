using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _levelsGridParent;

    [SerializeField]
    private LevelsListSO _levelsList;

    [SerializeField]
    private GameObject _levelButtonPrefab;

    private void Start()
    {
        if (_levelsGridParent != null)
        {
            _levelsList.levelData.ForEach((level) =>
            {
                var levelButton = Instantiate(_levelButtonPrefab);
                levelButton.transform.SetParent(_levelsGridParent.transform, false);
                levelButton.GetComponent<LevelButton>().level = level;
            });
        }
    }
}
