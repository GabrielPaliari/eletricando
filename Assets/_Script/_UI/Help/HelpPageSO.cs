using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HelpSystem/New Help Page", menuName = "HelpSystem/HelpPage")]
public class HelpPageSO : ScriptableObject
{
    [field: SerializeField]
    public Sprite tabImage;

    [field: SerializeField]
    public string title { get; private set; }

    [TextArea(minLines: 5, maxLines: 15)]
    [SerializeField]
    public string content;
}