using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Switch : MonoBehaviour, ILogicGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] {
        OnOffState
    };
    public StateFunction[] stateFunctions => new StateFunction[] {
        OnOffState
    };
    public int inputsLength => 1;
    public int outputsLength => 1;
    public int stateLength => 1;

    private bool isOn = false;

    private LogicGate logicGate;

    [SerializeField] private MeshRenderer indicatorMeshRenderer;
    [SerializeField] private Material onMaterial;
    [SerializeField] private Material offMaterial;

    [SerializeField] private Transform indicatorTransform;
    [SerializeField] private float onIndicatorX;
    [SerializeField] private float offIndicatorX;

    private float animDuration = .25f;

    void Start()
    {
        logicGate = GetComponent<LogicGate>();
    }

    private bool OnOffState(BitArray inputs)
    {
        return isOn;
    }

    private void OnMouseEnter()
    {
        LogicCircuitSystem.Instance.OnClicked += UpdateState;
    }
    private void OnMouseExit()
    {
        LogicCircuitSystem.Instance.OnClicked -= UpdateState;
    }

    private void UpdateState()
    {
        isOn = !isOn;
        SoundFeedback.Instance.PlaySound(isOn ? SoundType.SwitchOn : SoundType.SwitchOff);
        if (logicGate != null)
        {
            logicGate.OnInputChange(0, false);
        }
        if (indicatorMeshRenderer != null && indicatorTransform != null)
        {
            indicatorMeshRenderer.material = isOn ? onMaterial :offMaterial;
            var rotation = new Vector3(isOn ? onIndicatorX : offIndicatorX, 0f, 0f);
            indicatorTransform.DOLocalRotate(rotation, animDuration).SetEase(Ease.InOutCubic);
        }
    }
}
