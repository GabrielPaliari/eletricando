
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField]
    private float previewYOffset = 0.06f;

    [SerializeField]
    private GameObject cellIndicator;
    private GameObject previewObject;

    [SerializeField]
    private Material previewMaterialPrefab;
    private Material previewMaterialInstance;

    private Renderer cellIndicatorRenderer;

    private Vector2Int objectSize = Vector2Int.one;
    private RotationDir previousRotationDir;

    public GameEvent onChangeCursorAdd, onChangeCursorAddHighlight, onChangeCursorRemove, onChangeCursorRemoveHighlight;


    private void Start()
    {
        previewMaterialInstance = new Material(previewMaterialPrefab);
        cellIndicator.SetActive(false);
        cellIndicatorRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        objectSize = size;
        previewObject = Instantiate(prefab);
        PreparePreview(previewObject);
        PrepareCursor(size);
        cellIndicator.SetActive(true);
    }

    private void PrepareCursor(Vector2Int size)
    {   
        previousRotationDir = RotationUtil.currentDir;

        int width, height;
        RotationUtil.GetObjectWidthAndHeight(size, out width, out height);
        var rotatedSize = new Vector2Int(width, height);

        if (rotatedSize.x > 0 || rotatedSize.y > 0)
        {
            cellIndicator.transform.localScale = new Vector3(rotatedSize.x, 1, rotatedSize.y);
            cellIndicatorRenderer.material.mainTextureScale = rotatedSize;
        }
    }

    private void PreparePreview(GameObject previewObject)
    {
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = previewMaterialInstance;
            }
            renderer.materials = materials;
        }
    }

    public void StopShowingPreview()
    {
        cellIndicator.SetActive(false);
        if (previewObject != null)
            Destroy(previewObject);
    }

    public void UpdatePosition(Vector3 position, bool validity)
    {
        if (previewObject != null)
        {
            MovePreview(position);
            ApplyFeedbackToPreview(validity);
        }
        if (previousRotationDir != RotationUtil.currentDir)
        {          
            PrepareCursor(objectSize);
        }
        MoveCursor(position);        
        ApplyFeedbackToCursor(validity, previewObject == null);
    }

    private void ApplyFeedbackToPreview(bool validity)
    {
        Color c = validity ? Color.white : Color.red;

        if (validity)
        {
            onChangeCursorAddHighlight.Raise();
        } else
        {
            onChangeCursorAdd.Raise();
        }

        c.a = 0.5f;
        previewMaterialInstance.color = c;
    }

    private void ApplyFeedbackToCursor(bool validity, bool isRemovingState = false)
    {
        Color c = validity ? Color.white : Color.red;

        c.a = 0.5f;
        cellIndicatorRenderer.material.color = c;

        if (isRemovingState)
        {
            if (validity)
            {
                onChangeCursorRemoveHighlight.Raise();
            }
            else
            {
                onChangeCursorRemove.Raise();
            }
        }
    }

    private void MoveCursor(Vector3 position)
    {
        cellIndicator.transform.position = position;
    }

    private void MovePreview(Vector3 position)
    {
        Vector2Int rotationOffset = RotationUtil.GetRotationOffset(objectSize);
        previewObject.transform.position = new Vector3(
            position.x + rotationOffset.x,
            position.y + previewYOffset,
            position.z + rotationOffset.y);
        previewObject.transform.rotation = RotationUtil.GetRotationAngle();
    }

    internal void StartShowingRemovePreview()
    {
        cellIndicator.SetActive(true);
        PrepareCursor(Vector2Int.one);
        ApplyFeedbackToCursor(false, true);
    }
}
