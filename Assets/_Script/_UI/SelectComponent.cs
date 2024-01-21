using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectComponent : MonoBehaviour
{
    public PlacementSystem placementSystem;
    public WireSystem wireSystem;

    public Image image;
    public Button button;


    public void SetComponent(PlaceableComponentSO componentSO) {
        image.sprite = componentSO.menuImage;
        button.onClick.AddListener(() => OnSelectComponent(componentSO.ID));
    }

    public void OnSelectComponent(int id)
    {
        switch (id)
        {
            case -1:
                placementSystem.StartRemoving();
                break;
            case 0:
                wireSystem.EnterWireMode();
                break;
            case -2:
                wireSystem.EnterWireDeleteMode();
                break;
            default:
                placementSystem.StartPlacement(id);
                break;
        }
    }
}
