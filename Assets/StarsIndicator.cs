using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsIndicator : MonoBehaviour
{
    [SerializeField] Image topLosango;
    [SerializeField] Image leftLosango;
    [SerializeField] Image rightLosango;

    public static StarsIndicator Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowStar(LosangoType losango)
    {
        switch (losango)
        {
            case LosangoType.Top:
                DoScaleInAnimation(topLosango);
                break;
            case LosangoType.Left:
                DoScaleInAnimation(leftLosango);
                break;
            case LosangoType.Right:
                DoScaleInAnimation(rightLosango);
                break;
            default:
                break;        
        }
    }
    private void DoScaleInAnimation(Image image)
    {
        image.rectTransform.DOScale(Vector3.one, 1).SetEase(Ease.OutElastic);
    }

    public void DisableStar(LosangoType losango)
    {
        switch (losango)
        {
            case LosangoType.Top:
                DoScaleOutAnimation(topLosango);
                break;
            case LosangoType.Left:
                DoScaleOutAnimation(leftLosango);
                break;
            case LosangoType.Right:
                DoScaleOutAnimation(rightLosango);
                break;
            default:
                break;
        }
    }

    private void DoScaleOutAnimation(Image image)
    {
        image.rectTransform.DOScale(Vector3.zero, 1).SetEase(Ease.InOutCubic);
    }

}

public enum LosangoType
{
    Top,
    Left,
    Right,
}
