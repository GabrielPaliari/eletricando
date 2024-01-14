using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public LevelSO level;
    public TextMeshProUGUI textMesh; 
    public bool isTextSet = false;
    
    private void Update()
    {
        if (!isTextSet && level)
        {
            isTextSet = true;
            textMesh.text = level.id.ToString();
        }        
    }

    public void SelectLevel()
    {
        LevelManager.Instance.LoadLevel(level);
    }
}
