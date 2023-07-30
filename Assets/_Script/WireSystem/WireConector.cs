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
    private Renderer conectorIndicatorRenderer;

    private void Start()
    {
        conectorIndicatorRenderer = GetComponent<Renderer>();
    }

    public void ApplyFeedbackToIndicator(bool validity)
    {
        Color c = validity ? Color.green : Color.red;

        c.a = 0.5f;
        conectorIndicatorRenderer.material.color = c;
    }

    private void OnMouseExit()
    {
        Color c = Color.white;

        c.a = 0.5f;
        conectorIndicatorRenderer.material.color = c;
    }
}
