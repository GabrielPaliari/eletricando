using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSelectorButton : MonoBehaviour
{
    private LevelSO _levelSO;
    [SerializeField] private Color _borderHighlightOnColor;
    [SerializeField] private Color _borderHighlightOffColor;
    [SerializeField] private TextMeshPro _levelNumberTxt;
    [SerializeField] private SpriteRenderer _border;

    private LevelStarsIndicator _starsIndicator;


    public void InitializeButton(bool previousLevelComplete, LevelSO levelData, CompletedLevelInfo levelInfo = null)
    {

        _levelSO = levelData;
        _levelNumberTxt.text = levelData.id.ToString();
        if (levelInfo != null)
        {
            var stars = levelInfo.starsNumber;
            _starsIndicator = GetComponent<LevelStarsIndicator>();
            _starsIndicator.ShowStars(stars);
        }

        ToggleBorderLevelEnabled(previousLevelComplete);
    }

    public void DesselectButton()
    {        
        //ToggleBorderHighlight(false);
    }

    public void SelectButton()
    {
        LevelSelectorManager.instance.SelectLevel(_levelSO);
        //ToggleBorderHighlight(true);
    }

    public void ToggleBorderLevelEnabled(bool selected)
    {
        _border.enabled = selected;
    }
}
