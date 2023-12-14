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
    public Vector3Int position;

    [SerializeField]
    public RotationDir rotation;

    [SerializeField]
    public List<int> signalSequence;
}