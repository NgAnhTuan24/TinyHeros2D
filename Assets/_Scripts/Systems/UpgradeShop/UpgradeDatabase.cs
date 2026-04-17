using System.Collections.Generic;
using UnityEngine;

public class UpgradeDatabase : MonoBehaviour
{
    public static UpgradeDatabase instance;

    public List<UpgradeData> upgrades;

    private Dictionary<UpgradeType, UpgradeData> dict;

    void Awake()
    {
        instance = this;

        dict = new Dictionary<UpgradeType, UpgradeData>();

        foreach (var u in upgrades)
        {
            dict[u.type] = u;
        }
    }

    public UpgradeData GetData(UpgradeType type)
    {
        return dict[type];
    }
}