using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElectricComponents/New Component Database", menuName = "Custom/Database")]
public class ObjectsDatabaseSO : ScriptableObject
{
    public List<PlaceableComponentSO> objectsData;
}