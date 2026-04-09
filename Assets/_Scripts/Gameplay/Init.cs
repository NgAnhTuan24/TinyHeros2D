using UnityEngine;
using Cinemachine;
using System.Collections;

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

            UIManager.instance.playerManager.RegisterPlayer(player);
        }

        if (data == null)
        {
            StartCoroutine(AutoSave());
        }
    }

    //Save data when entering the game
    IEnumerator AutoSave()
    {
        yield return null; //Wait 1 second before saving

        GameManager.instance.SaveGame();
    }
}
