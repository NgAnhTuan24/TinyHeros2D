using System.Collections.Generic;
using UnityEngine;

public class DialogueStateManager : MonoBehaviour
{
    private HashSet<string> triggered = new HashSet<string>();

    public void LoadFromData(GameData data)
    {
        triggered = new HashSet<string>(data.triggeredDialogues);
    }

    public void SaveToData(GameData data)
    {
        data.triggeredDialogues = new List<string>(triggered);
    }

    public bool IsTriggered(string id)
    {
        return triggered.Contains(id);
    }

    public void MarkTriggered(string id)
    {
        if (!triggered.Contains(id))
            triggered.Add(id);
    }
}
