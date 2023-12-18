using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private Transform _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main.transform;
    }

    void Update()
    {
        Vector3 cameraPosition = _mainCamera.position;
        gameObject.transform.rotation = Quaternion.LookRotation(gameObject.transform.position - cameraPosition);
    }
}
