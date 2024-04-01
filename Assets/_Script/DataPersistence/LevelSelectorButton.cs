using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSelectorButton : MonoBehaviour
{
    public LevelSO _levelSO;
    [SerializeField] private Color _borderHighlightOnColor;
    [SerializeField] private Color _borderHighlightOffColor;
    [SerializeField] private TextMeshPro _levelNumberTxt;
    [SerializeField] private SpriteRenderer _border;

    private LevelStarsIndicator _starsIndicator;
    private Collider _collider;

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

        _collider = GetComponent<Collider>();
        ToggleLevelEnabled(previousLevelComplete);
    }

    public Sequence CompleteLevel(int stars)
    {
        _starsIndicator = GetComponent<LevelStarsIndicator>();
        return _starsIndicator.ShowStarsAnimated(stars);
    }

    public void SelectButton()
    {
        LevelSelectorManager.instance.SelectLevel(_levelSO);
    }

    public void ToggleLevelEnabled(bool enabled)
    {
        _border.enabled = enabled;
        _collider.enabled = enabled;
    }

    public void ToggleLevelEnabledAnimated()
    {
        _collider.enabled = true;
        _border.transform.localScale = Vector3.zero;
        _border.enabled = true;
        _border.transform.DOScale(new Vector3(1.1f,1.1f,1.1f), 1f).SetEase(Ease.OutBack);
    }
}
