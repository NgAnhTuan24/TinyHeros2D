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
        // đây là phần sẽ checkpoint khi vào game (hiện tại không dùng được) -> dẫn tới khi vào game ch có điểm check point player die thì không đc spawn lại
        //if (GameManager.instance.checkpointManager != null && string.IsNullOrEmpty(GameManager.instance.checkpointManager.checkpointScene))
        //{
        //    GameManager.instance.checkpointManager.SetCheckpoint(transform.position);
        //}

        GameData data = SaveLoadManager.currentData;

        if (PlayerController.instance == null)
        {
            GameObject prefab;

            if (data != null)
            {
                prefab = CharacterData.instance.GetCharacterPrefab(data.characterName);
                GameManager.instance.coinManager.SetCoin(data.coin);
            }
            else
            {
                prefab = CharacterSelect.selectedCharacter;
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

        StartCoroutine(AutoSave()); //....
    }

    //lưu dữ liệu khi vào gameplay (sau này sẽ bỏ đi)
    IEnumerator AutoSave()
    {
        yield return null;

        GameManager.instance.SaveGame();
    }
}
