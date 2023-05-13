using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuilding", menuName = "ScriptableObjects/NewBuilding")]
public class BuildingScriptable : ScriptableObject{
    [SerializeField] public GameObject _initialPrefab;
}