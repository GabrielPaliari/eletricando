using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CursorManager : MonoBehaviour
{
    [SerializeField] private List<CursorSpec> cursorSpecs;

    public void ChangeCursor(string cursorType)
    {
        var cursorSpec = cursorSpecs.Find((spec) => spec.type.ToString().Equals(cursorType));
        cursorSpec = cursorSpec != null ? cursorSpec : cursorSpecs[0];
        Cursor.SetCursor(cursorSpec.texture, cursorSpec.hotspot, CursorMode.Auto);
    }
}
