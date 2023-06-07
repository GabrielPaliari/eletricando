using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireConector : MonoBehaviour
{
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
