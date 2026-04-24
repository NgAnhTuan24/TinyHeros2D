using System.Collections.Generic;
using UnityEngine;

public class DialogueStateManager : MonoBehaviour
{
    private HashSet<string> started = new HashSet<string>();
    private HashSet<string> completed = new HashSet<string>();

    public void LoadFromData(GameData data)
    {
        started = new HashSet<string>(data.startedDialogues);
        completed = new HashSet<string>(data.completedDialogues);
    }

    public void SaveToData(GameData data)
    {
        data.startedDialogues = new List<string>(started);
        data.completedDialogues = new List<string>(completed);
    }

    public bool IsStarted(string id) => started.Contains(id);
    public bool IsCompleted(string id) => completed.Contains(id);

    public void MarkStarted(string id)
    {
        if (!started.Contains(id))
            started.Add(id);
    }

    public void MarkCompleted(string id)
    {
        if (!completed.Contains(id))
            completed.Add(id);
    }
}
