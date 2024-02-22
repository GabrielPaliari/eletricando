using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class ConstantInteractionManager : MonoBehaviour
{
    [SerializeField] private GameObject editForm;

    private void OnMouseEnter()
    {
        Debug.Log("entrou");
        LogicCircuitSystem.Instance.OnClicked += ChangeEditForm;
    }

    private void OnMouseExit()
    {
        LogicCircuitSystem.Instance.OnClicked -= ChangeEditForm;
    }

    private void ChangeEditForm()
    {
        ParameterEvents.Instance.ChangeEditComponent(editForm);
    }
}
