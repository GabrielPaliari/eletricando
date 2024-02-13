using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private Dictionary<int, SignalComponentData> signalComps = new Dictionary<int, SignalComponentData>();

    public GameEvent completeLevelEvent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;         
        }
    }

    private void Start()
    {
        RegisterSignalComponent = new UnityEvent<SignalComponentData>();
        RegisterSignalComponent.AddListener((sigComp) => _RegisterSignalComponent(sigComp));

        UpdateSignalComponent = new UnityEvent<SignalComponentData>();
        UpdateSignalComponent.AddListener((sigComp) => _UpdateSignalComponent(sigComp));
    }

    private void _RegisterSignalComponent(SignalComponentData sigComp)
    {
        //var signalsRowUI = Instantiate(signalsRowUI_pf);
        //signalsRowUI.transform.SetParent(signalsCompsGrid.transform, false);

        //var signalsRowManager = signalsRowUI.GetComponent<SignalsRowManager>();
        //signalsRowManager.Initialize(sigComp);

        //signalCompsRows.Add(sigComp.id, signalsRowManager);
        signalComps.Add(sigComp.id, sigComp);
    }

    private void _UpdateSignalComponent(SignalComponentData sigComp)
    {
        if (signalComps.All((comp) => comp.Value.isAllcorrect))
        {
            completeLevelEvent.Raise();
        }
        //signalCompsRows.TryGetValue(sigComp.id, out var signalsRow);
        //if (signalsRow != null)
        //{
        //    signalsRow.UpdateSignalComponent(sigComp);
        //}
    }
}
