using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    private bool _levelCompleted = false;

    [SerializeField]
    private TextMeshProUGUI _completeLevelTXT;
    
    [SerializeField]
    private GameObject _nextLevelButton;

    [SerializeField]
    private GameObject _completeLevelModal;

    public void LevelCompleted()
    {
        if (_levelCompleted)
        {
            return;
        }
        _levelCompleted = true;
        bool isLastLevel = LevelManager.Instance.IsLastLevel();
        
        _completeLevelTXT.text = isLastLevel ? "Você completou o jogo! \nObrigado por jogar." : "Você completou o nível!";
        _nextLevelButton.SetActive(!isLastLevel);

        SoundFeedback.Instance.PlaySound(SoundType.VictorySound);
        TickSystem.Instance.isOn = false;
        _completeLevelModal.SetActive(true);
    }
}
