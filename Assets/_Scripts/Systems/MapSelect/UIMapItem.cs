using UnityEngine;

public class UIMapItem : MonoBehaviour
{
    [SerializeField] private int chapterID;
    [SerializeField] private GameObject lockPanel;

    void Start()
    {
        UpdateState();
    }

    public void UpdateState()
    {
        bool isUnlocked = GameManager.instance.IsChapterUnlocked(chapterID);

        lockPanel.SetActive(!isUnlocked);
    }

    void OnEnable()
    {
        UpdateState();
    }
}