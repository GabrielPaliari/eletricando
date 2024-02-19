using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerSimulator : MonoBehaviour
{
    [SerializeField] private Transform _fingerShape;
    [SerializeField] private List<Vector3> _positions;
    [SerializeField] private float _cycleLength = 1f;

    void Start()
    {
        var sequence = DOTween.Sequence().SetLoops(-1, LoopType.Restart);

        _positions.ForEach(position =>
        {
            sequence.Append(transform.DOMove(position, _cycleLength));
        });
        
    }

}
