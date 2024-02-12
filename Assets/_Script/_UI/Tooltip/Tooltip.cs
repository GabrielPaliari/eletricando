using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public Image backgroundImage;
    public LayoutElement layoutElement;
    public float fadeTime = 0.2f;
    public int characterWrapLimit;

    private RectTransform m_rectTransform;
    private void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();
    }
    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        } else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }
        contentField.text = content;
        UpdateLayout();
    }
    
    private void UpdateLayout()
    {
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit);
    }

    private void Update()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x + 20, Input.mousePosition.y);
        transform.position = mousePos;
        m_rectTransform.pivot = CalcPivot(Input.mousePosition);
    }

    private Vector2 CalcPivot(Vector2 mousePos)
    {
        float pivotX = mousePos.x / Screen.width;
        float pivotY = mousePos.y / Screen.height;

        return new Vector2(pivotX, pivotY);
    }

    private void OnEnable()
    {
        backgroundImage.DOFade(1f, fadeTime).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        backgroundImage.DOFade(0f, fadeTime).SetEase(Ease.Linear);
    }
}
