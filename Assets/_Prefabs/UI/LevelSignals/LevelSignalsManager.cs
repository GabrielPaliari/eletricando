using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelSignalsManager : MonoBehaviour
{
    private static LevelSignalsManager instance;
    public static LevelSignalsManager Instance => instance;

    [NonSerialized]
    public UnityEvent<SignalComponentData> RegisterSignalComponent;
    [NonSerialized]
    public UnityEvent<SignalComponentData> UpdateSignalComponent;

    [SerializeField]
    private GameObject signalsCompsGrid;
    [SerializeField]
    private GameObject signalsRowUI_pf;
    private Dictionary<int, SignalsRowManager> signalCompsRows = new Dictionary<int, SignalsRowManager>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        RegisterSignalComponent = new UnityEvent<SignalComponentData>();
        RegisterSignalComponent.AddListener((sigComp) => _RegisterSignalComponent(sigComp));

        UpdateSignalComponent = new UnityEvent<SignalComponentData>();
        UpdateSignalComponent.AddListener((sigComp) => _UpdateSignalComponent(sigComp));
    }

    private void _RegisterSignalComponent(SignalComponentData sigComp)
    {
        var signalsRowUI = Instantiate(signalsRowUI_pf);
        signalsRowUI.transform.SetParent(signalsCompsGrid.transform, false);

        var signalsRowManager = signalsRowUI.GetComponent<SignalsRowManager>();
        signalsRowManager.Initialize(sigComp);
        
        signalCompsRows.Add(sigComp.id, signalsRowManager);
    }

    private void _UpdateSignalComponent(SignalComponentData sigComp)
    {
        signalCompsRows.TryGetValue(sigComp.id, out var signalsRow);
        if (signalsRow != null)
        {
            signalsRow.UpdateSignalComponent(sigComp);
        }
    }
}
