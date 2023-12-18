using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignalValueManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI valueTMP;

    [SerializeField]
    private Image background;
    [SerializeField]
    private Image border;
    
    [SerializeField]
    private SignalsColorsDef colorsDef;
    private Color _bgColor;
    private Color _borderColor;
    private int _expectedValue;

    public void Initialize(int expectedValue, bool isActive)
    {
        valueTMP.text = expectedValue.ToString();
        _expectedValue = expectedValue;
        _bgColor = colorsDef.GetSignalColor(expectedValue);
        _bgColor.a = GetOpacity(isActive);
        background.color = _bgColor;
    }

    public void UpdateBackground(bool isActive)
    {
        _bgColor.a = GetOpacity(isActive);
        background.color = _bgColor;
    }

    public void UpdateBorder(bool isBorderVisible = false, bool isBorderActive = false, int value = 0)
    {
        _borderColor = colorsDef.GetAttemptColor(value == _expectedValue);
        var opacity = isBorderVisible ? GetOpacity(isBorderActive) : 0;
        _borderColor.a = opacity;
        border.color = _borderColor;
    }

    private float GetOpacity(bool isActive)
    {
        return isActive ? 1f : .5f;
    }
}
