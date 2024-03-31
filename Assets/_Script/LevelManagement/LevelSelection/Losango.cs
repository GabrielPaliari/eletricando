using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Losango : MonoBehaviour
{
    [SerializeField] SpriteRenderer triangle1;
    [SerializeField] SpriteRenderer triangle2;

    public void SetColor(Color color)
    {
        triangle1.color = color;
        triangle2.color = color;
    }
}
