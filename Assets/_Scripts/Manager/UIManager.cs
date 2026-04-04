using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    None,
    Save
}

[System.Serializable]
public class UIEntry
{
    public UIType type;
    public GameObject uiObject;
}

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

    [Header("List UI Entry")]
    [SerializeField] private List<UIEntry> uiList;

    private Dictionary<UIType, GameObject> uiDict;

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

        uiDict = new Dictionary<UIType, GameObject>();

        foreach (var entry in uiList)
        {
            uiDict[entry.type] = entry.uiObject;
        }
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

    public void ToggleUI(UIType type)
    {
        if (uiDict.TryGetValue(type, out GameObject ui))
        {
            bool isActive = ui.activeSelf;
            ui.SetActive(!isActive);

            SetUIActive(isActive);

            Time.timeScale = isActive ? 1f : 0f;
        }
    }

    public void CloseUI(UIType type)
    {
        if (uiDict.TryGetValue(type, out GameObject ui))
        {
            ui.SetActive(false);

            SetUIActive(true);

            Time.timeScale = 1f;
        }
    }
}
