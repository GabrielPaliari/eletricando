using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Losango : MonoBehaviour
{
    [SerializeField] SpriteRenderer triangle1;
    [SerializeField] SpriteRenderer triangle2;
    [SerializeField] Transform animatedLosango;

    public void SetColor(Color color)
    {
        triangle1.color = color;
        triangle2.color = color;
    }

    public Tween CompleteAnimated(float delay = 0)
    {
        return animatedLosango.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutElastic).SetDelay(delay);
    }
}
