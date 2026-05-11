using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] HeartUI heartUI;

    void Awake()
    {
        cam = FindObjectOfType<CinemachineVirtualCamera>();
        heartUI = FindObjectOfType<HeartUI>();
    }

    void Start()
    {
        GameData data = SaveLoadManager.currentData;

        if (SaveLoadManager.selectedSlot != -1)
        {
            data = SaveSystem.Load(SaveLoadManager.selectedSlot);
            SaveLoadManager.currentData = data;
        }

        if (PlayerController.instance == null)
        {
            GameObject prefab;

            if (data != null) //load game (continue game)
            {
                prefab = CharacterData.instance.GetCharacterPrefab(data.characterName);
                GameManager.instance.coinManager.SetCoin(data.coin);
                GameManager.instance.SetLoadedPlayTime(data.playTime);

                //load checkpoint
                GameManager.instance.checkpointManager.checkpointScene = data.checkpointScene;
                GameManager.instance.checkpointManager.checkpointPosition = data.checkpointPosition;

                GameManager.instance.dialogueStateManager.LoadFromData(data);
                GameManager.instance.tutorialStateManager.LoadFromData(data);

                GameManager.instance.endingUnlocked = data.endingUnlocked;
                GameManager.instance.endingPlayed = data.endingPlayed;

                // reload The UI button game
                var tutorial = GameManager.instance.tutorialStateManager;

                if (tutorial.jumpUnlocked)
                    UIManager.instance.controlUIManager.ShowJump();

                if (tutorial.attackUnlocked)
                    UIManager.instance.controlUIManager.ShowAttack();

                if (tutorial.throwUnlocked)
                    UIManager.instance.controlUIManager.ShowThrow();

                // load upgrade data
                if (data.upgrades != null)
                {
                    GameManager.instance.upgradeLevels.Clear();

                    foreach (var u in data.upgrades)
                    {
                        GameManager.instance.upgradeLevels[u.type] = u.level;
                    }
                }

                if (data.unlockedChapters != null)
                {
                    GameManager.instance.unlockedChapters = new HashSet<int>(data.unlockedChapters);
                }

                //Update the data saved to the UI
                UIManager.instance.saveSlotUI.UpdateUI(data, CharacterData.instance.GetIcon(data.characterName));
            }
            else //new game
            {
                prefab = CharacterSelect.selectedCharacter;
                GameManager.instance.SetLoadedPlayTime(0f);

                if (GameManager.instance.checkpointManager != null && string.IsNullOrEmpty(GameManager.instance.checkpointManager.checkpointScene))
                {
                    GameManager.instance.checkpointManager.SetCheckpoint(transform.position);
                }

                GameManager.instance.unlockedChapters.Clear();
                GameManager.instance.unlockedChapters.Add(1);
            }

            Vector3 spawnPos = data != null ? data.playerPosition : transform.position;

            GameObject player = Instantiate(prefab, spawnPos, Quaternion.identity);

            if (cam != null)
                cam.Follow = player.transform;

            var health = player.GetComponent<PlayerHealth>();
            if (heartUI != null)
                health.SetHeartUI(heartUI);

            if (data != null)
            {
                health.SetHP(data.playerHP, data.playerMaxHP);
            }

            GameManager.instance.ApplyAllUpgrades();

            UIManager.instance.playerManager.RegisterPlayer(player);
        }

        // bỏ
        //if (data == null)
        //{
        //    StartCoroutine(AutoSave());
        //}
    }

    //Save data when entering the game
    //IEnumerator AutoSave()
    //{
    //    yield return null; //Wait 1 frame before saving

    //    GameManager.instance.SaveGame();

    //    SaveLoadManager.currentData = SaveSystem.Load(SaveLoadManager.selectedSlot);
    //}
}
