using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerManager playerManager;

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

        playerManager = GetComponent<PlayerManager>();
    }
}
