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

    private Tween losangoAnimation;
    private int id = -1;

    private void Start()
    {
        StartCoroutine(RegisterStarTracker());
        losangoAnimation = lightMeshRenderer.transform.DOLocalRotate(new Vector3(0, 180, 0), 2).SetEase(Ease.Linear).SetLoops(-1);
        losangoAnimation.Pause();
    }

    public byte OnOffState(byte[] inputs)
    {
        if (lightMeshRenderer != null)
        {
            var isOn = inputs[0] > 0;
            if (isOn)
            {
                lightMeshRenderer.material = onMaterial;
                losangoAnimation.Play();
                if (id != -1)
                {
                    CompleteLevelManager.Instance.TurnOnStarTracker(id);
                }
            } else
            {
                lightMeshRenderer.material = offMaterial;
                losangoAnimation.Pause();
                if (id != -1)
                {
                    CompleteLevelManager.Instance.TurnOffStarTracker(id);
                }
            }
        }
        return inputs[0];
    }

    IEnumerator RegisterStarTracker()
    {
        yield return new WaitForSeconds(.1f);
        var logicGate = gameObject.GetComponent<LogicGate>();
        id = logicGate.id;
        CompleteLevelManager.Instance.RegisterStarTracker(id);
    }
}
