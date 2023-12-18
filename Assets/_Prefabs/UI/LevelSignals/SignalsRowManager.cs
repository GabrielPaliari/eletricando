using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SignalsRowManager : MonoBehaviour
{
    public GameObject signalValue_pf;
    [SerializeField]
    private TextMeshProUGUI nameTMP;
    private List<SignalValueManager> _signalValuesManagers = new List<SignalValueManager>();

    public void Initialize(SignalComponentData sigComp)
    {
        nameTMP.text = sigComp.displayName;

        _signalValuesManagers = new List<SignalValueManager>();
        int sigIndex = 0;
        var transform = gameObject.transform;
        sigComp.signalSequence.ForEach(sigValue => {
            var obj = Instantiate(signalValue_pf, transform, false);
            var sigValueManager = obj.GetComponent<SignalValueManager>();
            sigValueManager.Initialize(sigValue, sigComp.currentIndex == sigIndex);
            _signalValuesManagers.Add(sigValueManager);
            sigIndex++;
        });
    }

    public void UpdateSignalComponent(SignalComponentData sigComp)
    {
        int sigIndex = 0;
        _signalValuesManagers.ForEach(sigValueMan => {
            sigValueMan.UpdateBackground(sigComp.currentIndex == sigIndex);
            if (sigComp.type == ESignalComponent.Receptor)
            {
                if (sigComp.currentIndex == sigIndex)
                {
                    sigValueMan.UpdateBorder(true, true, sigComp.currentValue);
                }
            }
            sigIndex++;
        });
    }
}
