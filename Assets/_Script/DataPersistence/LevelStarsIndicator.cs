using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStarsIndicator : MonoBehaviour
{
    [SerializeField] Losango topLosango;
    [SerializeField] Losango leftLosango;
    [SerializeField] Losango rightLosango;
    [SerializeField] private Color _completedLosangoColor;
    [SerializeField] private Color _uncompletedLosangoColor;
    private Sequence sequence;

    public void ShowStars(int number)
    {
        if (number >= 1)
        {
            ShowStar(LosangoType.Top);
        }
        if (number >= 2)
        {
            ShowStar(LosangoType.Left);
        }
        if (number >= 3)
        {
            ShowStar(LosangoType.Right);
        }
    }

    public Sequence ShowStarsAnimated(int number)
    {
        sequence = DOTween.Sequence(); 
        if (number >= 1)
        {
            sequence.Append(CompleteLosangoAnimated(LosangoType.Top));
        }
        if (number >= 2)
        {
            sequence.Append(CompleteLosangoAnimated(LosangoType.Left));
        }
        if (number >= 3)
        {
            sequence.Append(CompleteLosangoAnimated(LosangoType.Right));
        }
        return sequence;
    }

    private void ShowStar(LosangoType losango)
    {
        switch (losango)
        {
            case LosangoType.Top:
                CompleteLosango(topLosango);
                break;
            case LosangoType.Left:
                CompleteLosango(leftLosango);
                break;
            case LosangoType.Right:
                CompleteLosango(rightLosango);
                break;
            default:
                break;
        }
    }
    private void CompleteLosango(Losango losango)
    {   
        losango.SetColor(_completedLosangoColor);
    }

    private Tween CompleteLosangoAnimated(LosangoType losango)
    {
        switch (losango)
        {
            case LosangoType.Top:
                return topLosango.CompleteAnimated();
            case LosangoType.Left:
                return leftLosango.CompleteAnimated();
            case LosangoType.Right:
                return rightLosango.CompleteAnimated();
            default:
                return null;
        }
        
    }
}
