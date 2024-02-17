using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _completeLevelTXT;
    
    [SerializeField]
    private GameObject _nextLevelButton;

    [SerializeField]
    private GameObject _completeLevelModal;

    public void LevelCompleted()
    {
        bool isLastLevel = LevelManager.Instance.IsLastLevel();
        
        _completeLevelTXT.text = isLastLevel ? "Voc� completou o jogo! \nObrigado por jogar." : "Voc� completou o n�vel!";
        _nextLevelButton.SetActive(!isLastLevel);

        SoundFeedback.Instance.PlaySound(SoundType.VictorySound);
        TickSystem.Instance.isOn = false;
        _completeLevelModal.SetActive(true);
    }
}
