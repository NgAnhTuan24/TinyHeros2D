using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entry
{
    public string name;
    public Sprite icon;
}

public class CharacterData : MonoBehaviour
{
    public static CharacterData instance;

    public List<Entry> entries;

    private Dictionary<string, Sprite> dict;

    private void Awake()
    {
        instance = this;

        dict = new Dictionary<string, Sprite>();

        foreach (var e in entries)
        {
            if (!dict.ContainsKey(e.name))
            {
                dict.Add(e.name, e.icon);
            }
        }
    }

    public Sprite GetIcon(string name)
    {
        if (dict.TryGetValue(name, out Sprite icon))
        {
            return icon;
        }

        Debug.LogWarning("Không tìm thấy icon cho: " + name);
        return null;
    }
}
