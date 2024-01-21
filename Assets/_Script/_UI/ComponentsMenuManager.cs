using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentsMenuManager : MonoBehaviour
{
    private List<PlaceableComponentSO> _placeableComponents;
    public GameObject componentButton_pf;

    [SerializeField]
    private PlacementSystem _placementSystem;
    [SerializeField]
    private WireSystem _wireSystem;
    [SerializeField]
    private EComponentType cTypeFilter;
    [SerializeField]

    void Start()
    {
        _placeableComponents = LevelManager.Instance._selectedLevel.componentsAvailable.objectsData;
        _placeableComponents.ForEach(component =>
        {
            if(component.ComponentType.Equals(cTypeFilter))
            {
                var componentBtn = Instantiate(componentButton_pf);
                componentBtn.transform.SetParent(gameObject.transform, false);
                var selectComponent = componentBtn.GetComponent<SelectComponent>();
                selectComponent.placementSystem = _placementSystem;
                selectComponent.wireSystem = _wireSystem;
                selectComponent.SetComponent(component);
            }
        });
    }
}
