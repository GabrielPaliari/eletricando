using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightHex : MonoBehaviour
{
    [SerializeField] GameObject highlight;
    public void On()
    {
        Debug.Log("Highlight ON");
        highlight.SetActive(true);
    }
    public void Off()
    {
        highlight.SetActive(false);
    }
}
