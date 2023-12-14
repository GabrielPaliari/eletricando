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
    private int tick;
    
    [SerializeField] private float tickDuration;
    [SerializeField] private int clockDuration;

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
        tick = 0;
        tickTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= tickDuration)
        {
            tickTimer -= tickDuration;
            tick++;
            OnPropagationTick.Raise();
            
            if (tick >= clockDuration)
            {
                OnClockTick.Raise();
                tick -= clockDuration;
            }
        }
    }
}
