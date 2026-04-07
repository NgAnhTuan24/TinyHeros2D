using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string GetPath(int slot)
    {
        return Application.persistentDataPath + "/save_" + slot + ".json";
    }

    public static void Save(GameData data, int slot)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetPath(slot), json);

        Debug.Log("Saved slot " + slot);
    }

    public static GameData Load(int slot)
    {
        string path = GetPath(slot);

        if (!File.Exists(path))
        {
            return null;
        }

        string json = File.ReadAllText(path);
        GameData data = JsonUtility.FromJson<GameData>(json);

        return data;
    }
}