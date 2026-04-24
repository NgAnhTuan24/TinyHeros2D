using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string sceneName;
    public string characterName;
    public Vector3 playerPosition;
    public int playerHP;
    public int playerMaxHP;
    public int coin;
    public float playTime;

    [Header("Checkpoint Data")]
    public string checkpointScene;
    public Vector3 checkpointPosition;

    [Header("Dialogue Game Data")]
    public List<string> startedDialogues;
    public List<string> completedDialogues;

    [Header("Tutorial Flag Data")]
    public bool jumpUnlocked;
    public bool attackUnlocked;
    public bool throwUnlocked;

    [Header("Upgrade Data")]
    public List<UpgradeSaveData> upgrades;
}

[System.Serializable]
public class UpgradeSaveData
{
    public UpgradeType type;
    public int level;
}