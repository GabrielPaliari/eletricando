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
    public LayoutElement layoutElement;

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
        Vector2 mousePos = Input.mousePosition;
        transform.position = mousePos;
        m_rectTransform.pivot = CalcPivot(mousePos);
    }

    private Vector2 CalcPivot(Vector2 mousePos)
    {
        float pivotX = mousePos.x / Screen.width;
        float pivotY = mousePos.y / Screen.height;

        return new Vector2(pivotX, pivotY);
    }
}
