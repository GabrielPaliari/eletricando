using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompleteLevelManager : MonoBehaviour
{
    public static CompleteLevelManager Instance;
    private Dictionary<int, bool> starTrackers = new();
    [SerializeField]
    private Transform mainCameraPivot;
    [SerializeField]
    private Material cubeGlowMaterial;
    [SerializeField]
    private MeshRenderer cubeMeshRenderer;
    [SerializeField]
    private GameObject completeLevelModal;

    [SerializeField]
    private Camera mainCamera;
    private Vector3 mainCameraInitialPos;
    private float mainCameraInitialSize;

    private void Start()
    {
        mainCameraInitialPos = mainCamera.transform.position;
        mainCameraInitialSize = mainCamera.orthographicSize;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterStarTracker(int compId)
    {
        if (compId <= 0 || starTrackers.ContainsKey(compId)) { return; }

        starTrackers.Add(compId, false);
    }

    public void TurnOnStarTracker(int compId)
    {
        if (starTrackers.ContainsKey(compId))
        {
            starTrackers[compId] = true;
            SoundFeedback.Instance.PlaySound(SoundType.WinStarSound);
            var completed = GetCompletedStars();
            switch (completed)
            {
                case 1:
                    StarsIndicator.Instance.ShowStar(LosangoType.Left);
                    break;
                case 2:
                    StarsIndicator.Instance.ShowStar(LosangoType.Right);
                    break;
            }
        }
    }

    public void TurnOffStarTracker(int compId)
    {
        if (starTrackers.ContainsKey(compId))
        {
            starTrackers[compId] = false;
            var completed = GetCompletedStars();
            switch (completed)
            {
                case 0:
                    StarsIndicator.Instance.DisableStar(LosangoType.Left);
                    StarsIndicator.Instance.DisableStar(LosangoType.Right);
                    break;
                case 1:
                    StarsIndicator.Instance.DisableStar(LosangoType.Right);
                    break;
            }
        }
    }

    public void CompleteLevel()
    {
        var completedTrackers = GetCompletedStars() + 1;
        StarsIndicator.Instance.ShowStar(LosangoType.Top);
        SoundFeedback.Instance.PlaySound(SoundType.CompleteLevelSound);
        DoCameraAnimation();
        completeLevelModal.SetActive(true);
        cubeMeshRenderer.material = cubeGlowMaterial;
    }

    private void DoCameraAnimation()
    {
        DOTween.To(() => mainCamera.orthographicSize, x => mainCamera.orthographicSize = x, mainCameraInitialSize, 2f);
        Sequence animSequence = DOTween.Sequence();
        animSequence.Append(mainCamera.transform.DOMove(mainCameraInitialPos, 2f));
        animSequence.Append(mainCameraPivot
            .DORotate(new Vector3(0, 180, 0), 4f)
            .SetEase(Ease.Linear)
            .SetLoops(int.MaxValue, LoopType.Incremental)
        );
    }

    private int GetCompletedStars()
    {
        return starTrackers.Values.ToArray().Count((isCompleted) => isCompleted);
    }
}
