using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentMenuBtn : MonoBehaviour
{
    private PlacementSystem _placementSystem;
    private WireSystem _wireSystem;
    private ComponentsMenuManager _menuManager;

    public Image compImage;
    public Image bgImage;
    public Button button;
    public GameEvent disableMenuHighlights;

    public void SetConfig(
        PlaceableComponentSO componentSO,
        PlacementSystem placeSys,
        WireSystem wireSys,
        ComponentsMenuManager menuManager
    ) {
        compImage.sprite = componentSO.menuImage;
        button.onClick.AddListener(() => OnSelectComponent(componentSO.ID));
        _placementSystem = placeSys;
        _wireSystem = wireSys;
        _menuManager = menuManager;
    }

    public void OnSelectComponent(int id)
    {
        disableMenuHighlights.Raise();
        switch (id)
        {
            case -1:
                _placementSystem.StartRemoving();
                break;
            case 0:
                _wireSystem.EnterWireMode();
                break;
            case -2:
                _wireSystem.EnterWireDeleteMode();
                break;
            default:
                _placementSystem.StartPlacement(id);
                break;
        }

        _menuManager.HighlightComponent(id);
    }

    public void HighlightBtn(bool isOn)
    {
        bgImage.enabled = isOn;
    }
}
