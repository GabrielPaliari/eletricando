using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string header;

    [TextArea]
    public string content;
    private bool m_isMouseInside = false;
    public float tooltipDelay = 0.75f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_isMouseInside = true;
        Invoke("ShowTooltip", tooltipDelay);
    }

    private void ShowTooltip()
    {
        if (m_isMouseInside)
        {
            TooltipSystem.Show(content, header);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_isMouseInside = false;
        TooltipSystem.Hide();
    }
}
