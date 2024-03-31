using UnityEngine;
using UnityEditor;

public class SaveLevelConfigManager : MonoBehaviour
{
    public static string pathToSaveComps = "Assets/_Script/_SO/Levels/TEST/Components/";

    public void SaveLevel()
    {
        DeleteAllFilesInFolder();
        var selectedLevel = LevelManager.Instance._selectedLevel;
        var logicGates = LogicCircuitSystem.Instance.logicGates;
        selectedLevel.preBuiltComponents = new();

        foreach (var gate in logicGates)  
        {
            var builded = ScriptableObject.CreateInstance<BuildedComponentSO>();
            builded.name = gate.name;
            builded.componentId = gate.componentId;
            builded.position = gate.position;
            builded.rotation = gate.rotationDir;            
            builded.buidedId = gate.id;
            if (gate.wireNodes.Count > 0) // wire
            {
                builded.wireNodes = gate.wireNodes;
                builded.connectorA = gate.connectorA;
                builded.connectorB = gate.connectorB;
            }
            builded.initialState = GetGateState(gate);
            SaveSO(builded, $"{gate.name}");
            selectedLevel.preBuiltComponents.Add(builded);
        }
    }

    private int GetGateState(LogicGate gate)
    {
       var stateGate = gate.gameObject.GetComponent<IStateGateSpec>();
        if (stateGate != null)
        {
            return stateGate.state;
        }
        return 0;
    }

    private string SaveSO(ScriptableObject so, string name)
    {
        string path = AssetDatabase.GenerateUniqueAssetPath(pathToSaveComps + name + ".asset");
        AssetDatabase.CreateAsset(so, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        return path;
    }

    static void DeleteAllFilesInFolder()
    {
        // Find all assets in the specified folder
        string[] assets = AssetDatabase.FindAssets("", new string[] { pathToSaveComps });

        // Convert asset GUIDs to asset paths
        string[] assetPaths = new string[assets.Length];
        for (int i = 0; i < assets.Length; i++)
        {
            assetPaths[i] = AssetDatabase.GUIDToAssetPath(assets[i]);
        }
        AssetDatabase.DeleteAssets(assetPaths, new());
    }
}
