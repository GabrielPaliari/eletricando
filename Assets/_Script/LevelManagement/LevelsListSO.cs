using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelsList", menuName = "Custom/LevelList")]
public class LevelsListSO : ScriptableObject
{
    public List<LevelSO> levelData;
}