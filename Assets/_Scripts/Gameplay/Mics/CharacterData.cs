using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entry
{
    public string name;
    public Sprite icon;
    public GameObject prefab;
}

public class CharacterData : MonoBehaviour
{
    public static CharacterData instance;

    public List<Entry> entries;

    private Dictionary<string, Sprite> iconDict;
    private Dictionary<string, GameObject> prefabDict;

    private void Awake()
    {
        instance = this;

        iconDict = new Dictionary<string, Sprite>();
        prefabDict = new Dictionary<string, GameObject>();

        foreach (var e in entries)
        {
            if (!iconDict.ContainsKey(e.name))
            {
                iconDict.Add(e.name, e.icon);
            }
            if (!prefabDict.ContainsKey(e.name))
            {
                prefabDict.Add(e.name, e.prefab);
            }
        }
    }

    public Sprite GetIcon(string name)
    {
        if (iconDict.TryGetValue(name, out Sprite icon))
        {
            return icon;
        }

        Debug.LogWarning("Không tìm thấy icon cho: " + name);
        return null;
    }

    public GameObject GetCharacterPrefab(string name)
    {
        if (prefabDict.TryGetValue(name, out var prefab))
            return prefab;

        Debug.LogError("Không tìm thấy character prefab: " + name);
        return null;
    }
}
