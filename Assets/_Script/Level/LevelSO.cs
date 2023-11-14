using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Custom/Level")]
[Serializable]
public class LevelSO : ScriptableObject
{
    [SerializeField]
    public int id;

    [SerializeField]
    public string title; 
    
    [SerializeField]
    public string subtitle;

    [TextArea(minLines: 4, maxLines: 10)]
    [SerializeField]
    public string description;

    [SerializeField]
    public List<int> inputsA;

    [SerializeField]
    public List<int> inputsB;

    [SerializeField]
    public List<int> outputs;

    [Tooltip("Id do componente")]
    [SerializeField]
    public List<int> componentsAvailable;
}