using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditComponentModal : MonoBehaviour
{
    private GameObject activeForm;

    void Start()
    {
        ParameterEvents.Instance.onEditComponentChange += ChangeComponent;
    }

    private void ChangeComponent(GameObject form)
    {   
        if(activeForm != null)
        {
            activeForm.SetActive(false); 
        }
        
        form.transform.SetParent(transform, false);
        form.SetActive(true);
        activeForm = form;
    }
}
