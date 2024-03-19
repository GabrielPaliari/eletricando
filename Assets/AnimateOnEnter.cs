using DG.Tweening;
using UnityEngine;

public class AnimateOnEnter : MonoBehaviour
{
    [SerializeField] RectTransform modalTransform;
    [SerializeField] RectTransform starIndicatorTransform;
    [SerializeField] RectTransform starIndicatorDestination;
    [SerializeField] GameObject overlay;

    private void Start()
    {
        modalTransform.localScale = Vector3.zero;
    }
    private void OnEnable()
    {
        overlay.SetActive(true);
        starIndicatorTransform.DOMove(starIndicatorDestination.position, 2f).SetEase(Ease.InOutCubic).SetDelay(1f);
        modalTransform.DOScale(1, 1f).SetEase(Ease.OutBack);
    }

    private void OnDisable()
    {
        overlay.SetActive(false);
        modalTransform.DOScale(0, .5f).SetEase(Ease.Linear);
    }
}
