using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabBtn : MonoBehaviour
{
    public TextMeshProUGUI tabTitle;
    public Image icon;
    private TabController _controller;
    private HelpPageSO _helpPage;
    public void Initialize(HelpPageSO page, TabController controller)
    {
        _controller = controller;
        _helpPage = page;
        icon.sprite = page.tabImage;
        tabTitle.text = page.title;
    }

    public void OnSelectedTab()
    {
        _controller.OnSelectTab(_helpPage);
    }
}
