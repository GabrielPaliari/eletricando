using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComponentType
{
    Normal,
    Receptor,
    Emissor,
    Wire
}
[CreateAssetMenu(fileName = "New Builded Component", menuName = "Custom/BuildedComponent")]
[Serializable]
public class BuildedComponentSO : ScriptableObject
{
    [SerializeField]
    public int componentId;

    [SerializeField]
    public string componentName = "";

    [SerializeField]
    public Vector3Int position;

    [SerializeField]
    public RotationDir rotation;

    [SerializeField]
    public List<int> signalSequence;

    [SerializeField]
    public List<Vector3> wireNodes;

    public Vector2Int connectorA;
    public Vector2Int connectorB;

    public int? buidedId;
}