using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Linq;
using Unity.VisualScripting;

public class SaveLevelManager : MonoBehaviour
{
    public static string pathToSaveComps = "Assets/_Script/_SO/Levels/TEST/Components/";

    public void SaveLevel()
    {
        DeleteAllFilesInFolder();
        var selectedLevel = LevelManager.Instance._selectedLevel;
        var logicGates = LogicCircuitSystem.Instance.logicGates;
        var orderedGates = logicGates.OrderBy((gate) => gate.componentId).Reverse();
        selectedLevel.preBuiltComponents = new();

        foreach (var gate in orderedGates)  
        {
            var builded = ScriptableObject.CreateInstance<BuildedComponentSO>();
            builded.name = gate.name;
            builded.componentId = gate.componentId;
            builded.position = gate.position;
            builded.rotation = gate.rotationDir;
            if (gate.wireNodes.Count > 0) // wire
            {
                builded.wireNodes = gate.wireNodes;
                builded.connectorA = gate.connectorA;
                builded.connectorB = gate.connectorB;
            }
            SaveSO(builded, $"{gate.name}");
            selectedLevel.preBuiltComponents.Add(builded);
        }
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
