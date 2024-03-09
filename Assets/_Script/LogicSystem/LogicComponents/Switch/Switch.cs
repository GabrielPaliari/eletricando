using UnityEngine;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI;

public class Switch : MonoBehaviour, IStateGateSpec, ILogicGateSpec
{
    public OutputFunction[] outputFunctions => new OutputFunction[] {
        UpdateOutputs
    };
    public int inputsLength => 1;
    public int outputsLength => 1;

    private int _currentState = 0;
    public int state => _currentState;

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

    public void InitState(int initialState)
    {
        _currentState = initialState;
        UpdateVisuals();
    }

    private byte UpdateOutputs(byte[] inputs)
    {
        return (byte)(_currentState);
    }

    public void UpdateState()
    {
        switch (_currentState)
        {
            case 0:
                _currentState = 1;
                break;
            case 1:
            default:
                _currentState = 0;
                break;
        }
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        var isOn = _currentState == 1;
        SoundFeedback.Instance.PlaySound(isOn ? SoundType.SwitchOn : SoundType.SwitchOff);
        if (logicGate != null)
        {
            logicGate.OnInputChange(0, 0);
        }
        if (indicatorMeshRenderer != null && indicatorTransform != null)
        {
            indicatorMeshRenderer.material = isOn ? onMaterial : offMaterial;
            var rotation = new Vector3(isOn ? onIndicatorX : offIndicatorX, 0f, 0f);
            indicatorTransform.DOLocalRotate(rotation, animDuration).SetEase(Ease.InOutCubic);
        }
    }
}
