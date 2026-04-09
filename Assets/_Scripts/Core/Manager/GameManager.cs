using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public SceneTransition sceneTransition;

    public CameraController cameraController;

    public CoinManager coinManager;

    public CheckpointManager checkpointManager;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
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

        SaveSystem.Save(data, SaveLoadManager.selectedSlot);

        UIManager.instance.saveSlotUI.UpdateUI(data, CharacterData.instance.GetIcon(data.characterName));
    }
}
