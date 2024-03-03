using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TickSystem : MonoBehaviour
{
    private static TickSystem instance;
    public static TickSystem Instance => instance;

    public GameEvent OnPropagationTick, OnClockTick;
    
    private float tickTimer;
    //private int tick;
    public bool isOn = true;
    
    [SerializeField] private float tickDuration;
    [SerializeField] private int clockDuration;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        //tick = 0;
        tickTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //OnPropagationTick.Raise();
        //if (isOn) {
        //    tickTimer += Time.deltaTime;
        //    if (tickTimer >= tickDuration)
        //    {
        //        tickTimer -= tickDuration;
        //        tick++;
        //        OnClockTick.Raise();

        //        if (tick >= clockDuration)
        //        {
        //            OnClockTick.Raise();
        //            tick -= clockDuration;
        //        }
        //    }
        //}
    }
}
