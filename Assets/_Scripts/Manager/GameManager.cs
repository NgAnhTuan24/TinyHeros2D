using UnityEngine;

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
}
