using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevelButton : MonoBehaviour
{
    public void CompleteLevel()
    {
        CompleteLevelManager.Instance.ContinueToLevelSelection();
    }
}
