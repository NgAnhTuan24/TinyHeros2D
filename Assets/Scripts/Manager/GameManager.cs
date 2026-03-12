using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerManager playerManager;

    public ControlUIManager controlUIManager;

    public TutorialDialogueUI tutorialDialogueUI;

    public PauseManager pauseManager;

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

        controlUIManager = GetComponent<ControlUIManager>();

        tutorialDialogueUI = GetComponent<TutorialDialogueUI>();

        pauseManager = GetComponent<PauseManager>();
    }
}
