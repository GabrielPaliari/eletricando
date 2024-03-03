using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDetailsManager : MonoBehaviour
{
    private LevelSO _level;
    
    [SerializeField]
    private TextMeshProUGUI _titleTMP;    
    
    [SerializeField]
    private TextMeshProUGUI _subtitleTMP;
    
    [SerializeField]
    private TextMeshProUGUI _descriptionTMP;

    void Start()
    {
        //_level = LevelManager.Instance._selectedLevel;
        //_titleTMP.text = _level.title;
        //_subtitleTMP.text = _level.subtitle;
        //_descriptionTMP.text = _level.description;
    }
    
    public void RaiseEvent(GameEvent gameEvent)
    {
        gameEvent.Raise();
    }
}
