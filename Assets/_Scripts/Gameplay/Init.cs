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
        // đây là checkpoint nhưng chỉ set khi checkpoint rỗng - nên sửa lại (phù hợp cho new game không nên dùng cho load game)
        if (GameManager.instance.checkpointManager != null && string.IsNullOrEmpty(GameManager.instance.checkpointManager.checkpointScene))
        {
            GameManager.instance.checkpointManager.SetCheckpoint(transform.position);
        }

        GameData data = SaveLoadManager.currentData;

        if (PlayerController.instance == null)
        {
            GameObject prefab;

            if (data != null)
            {
                prefab = CharacterData.instance.GetCharacterPrefab(data.characterName);
                GameManager.instance.coinManager.SetCoin(data.coin);
                GameManager.instance.SetLoadedPlayTime(data.playTime);
            }
            else
            {
                prefab = CharacterSelect.selectedCharacter;
                GameManager.instance.SetLoadedPlayTime(0f);
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

        //StartCoroutine(AutoSave());
    }

    //lưu dữ liệu khi vào gameplay (sau này sẽ bỏ đi)
    //IEnumerator AutoSave()
    //{
    //    yield return null;

    //    GameManager.instance.SaveGame();
    //}
}
