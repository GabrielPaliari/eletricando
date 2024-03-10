using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosangoLED : MonoBehaviour, ILogicGateSpec
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

    private Tween animation;

    private void Start()
    {
        animation = lightMeshRenderer.transform.DOLocalRotate(new Vector3(0, 180, 0), 2).SetEase(Ease.Linear).SetLoops(-1);
    }

    public byte OnOffState(byte[] inputs)
    {
        if (lightMeshRenderer != null)
        {
            var isOn = inputs[0] > 0;
            if (isOn)
            {
                lightMeshRenderer.material = onMaterial;
                animation.Play();
            } else
            {
                lightMeshRenderer.material = offMaterial;
                animation.Pause();
            }
        }
        return inputs[0];
    }
}
