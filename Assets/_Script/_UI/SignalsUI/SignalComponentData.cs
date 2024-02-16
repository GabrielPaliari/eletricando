using System.Collections.Generic;
using System.Linq;

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
    public List<bool> correctSequence;
    public int currentIndex;
    public int currentValue;
    public bool hasChanged = true;
    public bool isCurrentCorrect = false;
    public bool isAllcorrect = false;

    public SignalComponentData(int id, ESignalComponent type, List<int> signalSequence, int currentIndex, int currentValue, string displayName)
    {
        this.id = id;
        this.type = type;
        this.signalSequence = signalSequence;
        this.currentIndex = currentIndex;
        this.currentValue = currentValue;
        this.displayName = displayName;
        correctSequence = Enumerable.Repeat(false, signalSequence.Count).ToList();
    }

    public void Update(int index, int value)
    {
        if (currentIndex == index && currentValue == value) {
            hasChanged = false;
            return;
        }
        currentIndex = index;
        currentValue = value;
        isCurrentCorrect = signalSequence[index] == value;
        correctSequence[index] = isCurrentCorrect;
        if (currentIndex == (signalSequence.Count - 1) && correctSequence.All(x => x))
        {
            isAllcorrect = true;
        } else
        {
            isAllcorrect = false;
        }
        hasChanged = true;
    }

    public string displayName;
}