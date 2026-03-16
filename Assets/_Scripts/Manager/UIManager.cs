using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public PlayerManager playerManager;

    public ControlUIManager controlUIManager;

    public TutorialDialogueUI tutorialDialogueUI;

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
    }
}
