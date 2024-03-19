using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LED : MonoBehaviour, ILogicGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] {
        OnOffState
    };
    public int inputsLength => 1;
    public int outputsLength => 1;

    [SerializeField]
    private MeshRenderer lightMeshRenderer;
    [SerializeField]
    private Material onMaterial;
    [SerializeField]
    private Material offMaterial;

    [SerializeField]
    private Transform completeIndicatorTransform;
    [SerializeField]
    private MeshRenderer completeIndicatorMesh;
    [SerializeField]
    private Material completeMaterial;
    [SerializeField]
    private Material uncompleteMaterial;
    [SerializeField]
    private float completeScale;

    private Tween completeLevelAnimation;
    private bool isFirstUpdate = true;
    private void Start()
    {
        completeLevelAnimation = completeIndicatorTransform
            .DOScale(new Vector3(completeScale, completeScale, completeScale), 3f)
            .SetAutoKill(false)
            .OnComplete(() => OnTweenComplete());
        completeLevelAnimation.Pause();
    }

    public byte OnOffState(byte[] inputs)
    {

        if (lightMeshRenderer != null)
        {
            var isOn = inputs[0] > 0;
            lightMeshRenderer.material = isOn ? onMaterial : offMaterial;
            completeIndicatorMesh.material = isOn ? completeMaterial : uncompleteMaterial;
            if (isOn)
            {
                completeLevelAnimation.PlayForward();
                SoundFeedback.Instance.PlaySound(SoundType.LedChargeUp);
            }
            else
            {
                completeLevelAnimation.PlayBackwards();
                if (!isFirstUpdate && SoundFeedback.Instance != null)
                {
                    SoundFeedback.Instance.PlaySound(SoundType.LedChargeDown);
                }
                isFirstUpdate = false;
            }
        }
        return inputs[0];
    }

    void OnTweenComplete()
    {
        // Check if the tween was played backward
        if (!completeLevelAnimation.IsBackwards())
        {
            CompleteLevelManager.Instance.CompleteLevel();
        }
    }
}
