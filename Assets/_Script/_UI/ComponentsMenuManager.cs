using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private Dictionary<int, ComponentMenuBtn> _buttons = new Dictionary<int, ComponentMenuBtn>();

    private int _selectedComponent;

    void Start()
    {
        _placeableComponents = LevelManager.Instance._selectedLevel.componentsAvailable.objectsData;
        _placeableComponents.ForEach(component =>
        {
            if(component.ComponentType.Equals(cTypeFilter))
            {
                var componentBtnObj = Instantiate(componentButton_pf);
                componentBtnObj.transform.SetParent(gameObject.transform, false);

                var componentMenuBtn = componentBtnObj.GetComponent<ComponentMenuBtn>();
                componentMenuBtn.SetConfig(component, _placementSystem, _wireSystem, this);
                
                _buttons.Add(component.ID, componentMenuBtn);

                var tooltipTrigger = componentBtnObj.GetComponent<TooltipTrigger>();
            }
        });
    }

    public void HighlightComponent(int compId = -1000)
    {        
        _buttons.TryGetValue(compId, out ComponentMenuBtn currentSelected);
        if(currentSelected != null)
        {
            currentSelected.HighlightBtn(true);
            _selectedComponent = compId;
        }
    }

    public void RemoveHighlight() {
        _buttons.ToList().ForEach((item) =>
        {
            item.Value.HighlightBtn(false);
        });      
    }
}
