using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterEvents : MonoBehaviour
{
    public static ParameterEvents Instance;

    private void Awake()
    {
        Instance = this;
    }

    public event Action<GameObject> onEditComponentChange;
    public void ChangeEditComponent(GameObject form_pf)
    {
        if (onEditComponentChange != null) {
            onEditComponentChange(form_pf);
        }
    }

}
