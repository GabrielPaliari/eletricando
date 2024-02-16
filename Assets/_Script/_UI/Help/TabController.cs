using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TabController : MonoBehaviour
{
    public HelpPagesDatabaseSO helpPages;
    public Transform tabButtonsGrid;
    public GameObject tabButton_pf;
    public TextMeshProUGUI title;
    public TextMeshProUGUI content;


    void Start()
    {
        helpPages.objectsData.ForEach((page) =>
        {
            InstantiatePage(page);
        });
    }

    private void InstantiatePage(HelpPageSO page)
    {
        var tabButtonObj = Instantiate(tabButton_pf);
        var tabButton = tabButtonObj.GetComponent<TabBtn>();
        tabButton.Initialize(page, this);
        tabButton.transform.SetParent(tabButtonsGrid, false);
    }

    public void OnSelectTab(HelpPageSO page)
    {
        title.text = page.title;
        content.text = page.content;
    }
}
