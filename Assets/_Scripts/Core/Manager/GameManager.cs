using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public SceneTransition sceneTransition;

    public CameraController cameraController;

    public CoinManager coinManager;

    public CheckpointManager checkpointManager;

    public DialogueManager dialogueManager;

    public DialogueStateManager dialogueStateManager;

    public TutorialStateManager tutorialStateManager;

    public ProjectileStatsManager projectileStatsManager;

    // Upgrade state data
    public Dictionary<UpgradeType, int> upgradeLevels = new Dictionary<UpgradeType, int>();

    private float sessionStartTime;
    private float loadedPlayTime;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);

        sceneTransition = GetComponent<SceneTransition>();

        cameraController = GetComponent<CameraController>();

        coinManager = GetComponent<CoinManager>();

        checkpointManager = GetComponent<CheckpointManager>();

        dialogueManager = GetComponent<DialogueManager>();

        dialogueStateManager = GetComponent<DialogueStateManager>();

        tutorialStateManager = GetComponent<TutorialStateManager>();

        projectileStatsManager = GetComponent<ProjectileStatsManager>();
    }

    public void SaveGame()
    {
        GameData data = new GameData();

        data.sceneName = SceneManager.GetActiveScene().name;
        data.characterName = PlayerController.instance.identity.playerName;
        data.playerPosition = PlayerController.instance.transform.position;
        data.playerHP = PlayerController.instance.health.GetCurrentHP();
        data.playerMaxHP = PlayerController.instance.health.GetMaxHP();
        data.coin = coinManager.CurrentCoin;
        float currentSession = Time.time - sessionStartTime;
        data.playTime = loadedPlayTime + currentSession;

        data.checkpointScene = checkpointManager.checkpointScene;
        data.checkpointPosition = checkpointManager.checkpointPosition;

        dialogueStateManager.SaveToData(data);
        tutorialStateManager.SaveToData(data);

        data.upgrades = new List<UpgradeSaveData>();

        foreach (var kvp in upgradeLevels)
        {
            data.upgrades.Add(new UpgradeSaveData
            {
                type = kvp.Key,
                level = kvp.Value
            });
        }

        SaveSystem.Save(data, SaveLoadManager.selectedSlot);

        UIManager.instance.saveSlotUI.UpdateUI(data, CharacterData.instance.GetIcon(data.characterName));
    }

    public void SetLoadedPlayTime(float time)
    {
        loadedPlayTime = time;
        sessionStartTime = Time.time;
    }

    public void ApplyAllUpgrades()
    {
        foreach (var kvp in upgradeLevels)
        {
            UpgradeType type = kvp.Key;
            int level = kvp.Value;

            for (int i = 0; i < level; i++)
            {
                ApplyUpgrade(type, i);
            }
        }
    }

    private void ApplyUpgrade(UpgradeType type, int levelIndex)
    {
        UpgradeData data = UpgradeDatabase.instance.GetData(type);

        float value = data.values[levelIndex];

        switch (type)
        {
            case UpgradeType.Damage:
                PlayerController.instance.hits.AddDamage(value);
                break;

            case UpgradeType.ProjectileDamage:
                projectileStatsManager.AddDamage(value);
                break;

            case UpgradeType.ProjectileSpeed:
                projectileStatsManager.AddSpeed(value);
                break;
        }
    }

    public bool IsUpgradeUnlocked(UpgradeType type)
    {
        var tutorial = tutorialStateManager;

        switch (type)
        {
            case UpgradeType.Damage:
                return tutorial.attackUnlocked;

            case UpgradeType.ProjectileDamage:
            case UpgradeType.ProjectileSpeed:
                return tutorial.throwUnlocked;

            default:
                return true;
        }
    }
}
