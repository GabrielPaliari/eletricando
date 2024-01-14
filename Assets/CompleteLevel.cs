using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public void LevelCompleted()
    {
        SoundFeedback.Instance.PlaySound(SoundType.VictorySound);
        TickSystem.Instance.isOn = false;
    }
}
