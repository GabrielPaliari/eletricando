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
    
    [SerializeField] private float tickDuration = .2f;
    [SerializeField] private int clockDuration = 1;

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
            OnPropagationTick?.Invoke();
            
            if (tick >= clockDuration)
            {
                OnClockTick?.Invoke();
                tick -= clockDuration;
            }
        }
    }
}
