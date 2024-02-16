using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HelpSystem/New Help Database", menuName = "HelpSystem/Database")]
public class HelpPagesDatabaseSO : ScriptableObject
{
    public List<HelpPageSO> objectsData;
}
