using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireSystem : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;

    private bool wireModeOn = false;
    public void EnterWireMode()
    {
        wireModeOn = true;
    }

    private void Update()
    {
        if (wireModeOn)
        {
            GameObject connectorObject = inputManager.GetSelectedWireConector();
            if (connectorObject != null)
            {
                var connector = connectorObject.GetComponent<WireConector>();
                connector.ApplyFeedbackToIndicator(true);
            }
        }
    }

}
