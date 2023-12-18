using System.Collections.Generic;

public enum ESignalComponent
{
    Emissor,
    Receptor
}

public class SignalComponentData
{
    public int id;
    public ESignalComponent type;
    public List<int> signalSequence;
    public int currentIndex;
    public int currentValue;

    public SignalComponentData(int id, ESignalComponent type, List<int> signalSequence, int currentIndex, int currentValue)
    {
        this.id = id;
        this.type = type;
        this.signalSequence = signalSequence;
        this.currentIndex = currentIndex;
        this.currentValue = currentValue;
    }

    public string displayName
    {
        get
        {
            switch (type)
            {
                case ESignalComponent.Emissor:
                    return $"Emissor {id}";
                case ESignalComponent.Receptor:
                    return $"Receptor {id}";
                default:
                    return $"Sinal {id}";
            }
        }
    }
}