using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface ISignalSeqGateSpec 
{
    List<int> _signalSequence { get; }
    ESignalComponent compType { get; }
    public void Initialize(int id, List<int> signalSeq);

    TextMeshPro nameTMP { get; }
}
