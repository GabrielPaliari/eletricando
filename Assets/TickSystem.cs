using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TickSystem : MonoBehaviour
{
    private static TickSystem instance;
    public static TickSystem Instance => instance;

    public event Action OnPropagationTick, OnClockTick;
    
    private float tickTimer;
    private int tick;
    
    [SerializeField] private float tickDuration = 1f;
    [SerializeField] private int clockDuration = 5;

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
        tickDuration = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= tickDuration)
        {
            tickTimer -= tickDuration;
            tick++;
            OnPropagationTick?.Invoke();
            
            if (tick >= clockDuration)
            {
                OnClockTick?.Invoke();
                tick -= clockDuration;
            }
        }
    }
}
