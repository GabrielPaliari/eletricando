using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Electric Component", menuName = "Custom/ElectricComponent")]
[Serializable]
public class PlaceableComponentSO : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public int ID { get; private set; }

    [field: SerializeField]
    public Sprite menuImage;
    
    [field: SerializeField]
    public Vector2Int Size { get; private set; } = Vector2Int.one;
    [field: SerializeField]
    public GameObject Prefab { get; private set; }

    [field: SerializeField]
    public EComponentType ComponentType;
}

public enum EComponentType
{
    Logic,
    Wiring,
    IO
}