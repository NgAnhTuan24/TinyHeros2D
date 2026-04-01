using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public PlayerManager playerManager;

    public ControlUIManager controlUIManager;

    public TutorialDialogueUI tutorialDialogueUI;

    [Header("UI_Game_Active")]
    [SerializeField] private GameObject[] uiGames;

    [Header("UI_You Died!")]
    [SerializeField] private GameObject deathUI;

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

    public void SetUIActive(bool isActive)
    {
        foreach (GameObject ui in uiGames)
        {
            if (ui != null)
            {
                ui.SetActive(isActive);
            }
        }
    }

    public void ShowDeathUI()
    {
        SetUIActive(false);
        deathUI.SetActive(true);
    }

    public void HideDeathUI()
    {
        deathUI.SetActive(false);
    }
}
