using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour
{
    private Vector3 _touchStart;
    [SerializeField] private float _zoomMin = 1;
    [SerializeField] private float _zoomMax = 10;
    [SerializeField] private float _zoomMultiplier = 0.01f;
    [SerializeField] private float _limitX = 10f;
    [SerializeField] private float _limitY = 10f;
    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _touchStart = _camera.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrev = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrev = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrev - touchOnePrev).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;
            zoom(difference * _zoomMultiplier);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = _touchStart - _camera.ScreenToWorldPoint(Input.mousePosition);
            _camera.transform.position += direction;
            LimitCameraPos();
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void zoom(float increment)
    {
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - increment, _zoomMin, _zoomMax);
    }

    void LimitCameraPos()
    {
        _camera.transform.position = new Vector3(Mathf.Clamp(_camera.transform.position.x, -_limitX, _limitX),
                Mathf.Clamp(_camera.transform.position.y, -_limitY, _limitY),
                Mathf.Clamp(_camera.transform.position.z, -_limitX, _limitX));
    }
}
