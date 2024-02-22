using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditableField : MonoBehaviour
{
    [SerializeField] private string label = "Valor";
    [SerializeField] private int defaultValue = 0;
    [SerializeField] private int minValue = 0;
    [SerializeField] private int maxValue = 8;
    [SerializeField] private ConstantEmitter constantEmitter;
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider.value = defaultValue;
        slider.maxValue = maxValue;
        slider.minValue = minValue;
        slider.onValueChanged.AddListener((value) =>
        {
            constantEmitter.UpdateState((int)Math.Floor(value));
        });
    }
}
