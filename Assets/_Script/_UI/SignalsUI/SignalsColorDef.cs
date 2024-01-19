using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SignalsColorDef")]
public class SignalsColorsDef : ScriptableObject
{
    [SerializeField]
    private List<Color> signalColors;

    [SerializeField]
    private Color rightAttemptColor;
    [SerializeField]
    private Color wrongAttemptColor;

    public Color GetSignalColor(int sigValue)
    {
        if (sigValue > 0 && sigValue < signalColors.Count)
        {
            return signalColors[sigValue];
        }
        return signalColors[0];
    }

    public Color GetAttemptColor(bool isRight)
    {
        return isRight ? rightAttemptColor : wrongAttemptColor; 
    }
}
