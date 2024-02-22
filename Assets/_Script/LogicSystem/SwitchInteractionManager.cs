using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SwitchInteractionManager : MonoBehaviour
{
    [SerializeField] private Switch _switchComp;

    private void OnMouseEnter()
    {
        LogicCircuitSystem.Instance.OnClicked += _switchComp.UpdateState;
    }
    private void OnMouseExit()
    {
        LogicCircuitSystem.Instance.OnClicked -= _switchComp.UpdateState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Finger")
        {
            _switchComp.UpdateState();
        }
    }
}
