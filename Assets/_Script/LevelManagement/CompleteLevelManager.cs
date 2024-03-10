using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompleteLevelManager : MonoBehaviour
{
    public static CompleteLevelManager Instance;
    private Dictionary<int, bool> starTrackers = new();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterStarTracker(int compId)
    {
        Debug.Log(compId);
        if (compId <= 0 || starTrackers.ContainsKey(compId)) { return; }

        starTrackers.Add(compId, false);
    }

    public void TurnOnStarTracker(int compId)
    {
        if (starTrackers.ContainsKey(compId))
        {
            starTrackers[compId] = true;
        }
    }

    public void TurnOffStarTracker(int compId)
    {
        if (starTrackers.ContainsKey(compId))
        {
            starTrackers[compId] = false;
        }
    }

    public void CompleteLevel()
    {
        var totalTrackers = starTrackers.Count + 1;
        var completedTrackers = starTrackers.Values.ToArray().Count((isCompleted) => isCompleted) + 1;
        Debug.Log($"Level Concluído. Estrelas: {completedTrackers}/{totalTrackers}");
    }

}
