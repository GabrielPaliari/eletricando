using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionCameraController : MonoBehaviour
{
    private Vector3 _touchStart;
    [SerializeField] private float _zoomMin = 1;
    [SerializeField] private float _zoomMax = 10;
    [SerializeField] private float _limitX = 10f;
    [SerializeField] private float _limitY = 10f;
    [SerializeField] private LayerMask levelButtonsLayer;
    private Camera _camera;
    [SerializeField] private Light _spotLight;

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
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = _touchStart - _camera.ScreenToWorldPoint(Input.mousePosition);
            _camera.transform.position += direction;
            LimitCameraPos();
        } 
        
        if (Input.GetMouseButtonUp(0))
        {
            var levelButtonHit = GetLevelButtonHit();
            if (levelButtonHit != null)
            {
                SelectLevelButton(levelButtonHit);
            }
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    private GameObject GetLevelButtonHit()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _camera.nearClipPlane;
        Ray ray = _camera.ScreenPointToRay(mousePos);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, levelButtonsLayer)) {
            return hit.collider.gameObject;
        };
        return null;
    }

    public Sequence SelectLevelButton(GameObject levelButton)
    {
        levelButton.GetComponent<LevelSelectorButton>().SelectButton();

        var buttonPos = levelButton.transform.position;
        var newCameraPos = new Vector3(buttonPos.x, buttonPos.y, Camera.main.transform.position.z);
        var sequence = DOTween.Sequence();
        sequence.Append(DOTween.To(() => _spotLight.intensity, x => _spotLight.intensity = x, 0, 0.1f));
        sequence.Append(Camera.main.transform.DOMove(newCameraPos, 0.8f));
        sequence.Append(DOTween.To(() => _spotLight.intensity, x => _spotLight.intensity = x, 20f, 0.5f));
        sequence.Play();
        return sequence;
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
