using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CursorSpec", menuName = "Custom/CursorSpec")]
public class CursorSpec : ScriptableObject
{
    [SerializeField] public ECursorTypes type;
    [SerializeField] public Texture2D texture;
    [SerializeField] public Vector2 hotspot;
}