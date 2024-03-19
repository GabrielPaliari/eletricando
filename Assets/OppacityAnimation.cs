using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OppacityAnimation : MonoBehaviour
{
    [SerializeField] Image overlay;
    [SerializeField] Color finalColor;

    private void Start()
    {
        overlay.color = Color.clear;
    }

    private void OnEnable()
    {
        overlay.DOColor(finalColor, 1f);    
    }
}
