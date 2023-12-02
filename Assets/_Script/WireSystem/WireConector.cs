using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESignalType
{
    Input,
    Output,
}
public class WireConector : MonoBehaviour
{
    [SerializeField] public ESignalType type;
    [SerializeField] public int index;
    [SerializeField] private Collider collider;

    private Renderer conectorIndicatorRenderer;

    private void Start()
    {
        conectorIndicatorRenderer = GetComponent<Renderer>();
    }

    private void applyHighlight()
    {
        conectorIndicatorRenderer.enabled = true;
        collider.enabled = true;
        Color c = Color.green;

        c.a = 0.5f;
        if (conectorIndicatorRenderer != null)
        {
            conectorIndicatorRenderer.material.color = c;
        }
    }
    public void removeHighlight()
    {
        conectorIndicatorRenderer.enabled = false;
        collider.enabled = false;
        Color c = Color.white;

        c.a = 0.5f;
        if (conectorIndicatorRenderer != null)
        {
            conectorIndicatorRenderer.material.color = c;
        }
    }

    public void HighlightInput()
    {
        if (type == ESignalType.Input)
        {
            applyHighlight();
        } else
        {
            removeHighlight();
        }
    }

    public void HighlightOutputs()
    {
        if (type == ESignalType.Output)
        {
            applyHighlight();
        }
        else
        {
            removeHighlight();
        }
    }

    private void OnMouseEnter()
    {
        Color c = conectorIndicatorRenderer.material.color;
        c.a = 0.8f;
        conectorIndicatorRenderer.material.color = c;
    }

    private void OnMouseExit()
    {
        Color c = conectorIndicatorRenderer.material.color;
        c.a = 0.5f;
        conectorIndicatorRenderer.material.color = c;
    }
}
