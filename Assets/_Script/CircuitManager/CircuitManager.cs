using UnityEngine;

public class CircuitManager : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
    //    var ckt = new Circuit(
    //        new VoltageSource("V1", "v1", "0", 1.0),
    //        //new VoltageSource("V2", "in", "v1", 1.0),
    //        new Resistor("R1", "v1", "0", 0.0)
    //    //new Resistor("R2", "in", "0", 1.0)
    //    );

    //    var dc = new OP("DC 1");
    //    var currentV1 = new RealPropertyExport(dc, "V1", "i");
    //    //var currentR1 = new RealPropertyExport(dc, "R1", "i");
    //    //var currentR2 = new RealPropertyExport(dc, "R2", "i");

    //    // Catch exported data
    //    dc.ExportSimulationData += (sender, args) =>
    //    {
    //        //Debug.Log($"CurrentV1: {currentV1.Value} Current R1: {currentR1.Value} Current R2: {currentR2.Value}");
    //        Debug.Log($"CurrentV1: {currentV1.Value}");
    //    };
    //    dc.Run(ckt);
    //}

    public void AddComponentToCircuit(Component sender, object data)
    {
        ElectricData component = data as ElectricData;
    }
}

public class ElectricData {
    public string componentName;

    public ElectricData(string name) {
        componentName = name;
    }
}