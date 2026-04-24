using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSlotUI : MonoBehaviour
{
    [Header("Icon Character")]
    [SerializeField] private Image characterImage;
    [Header("Name Character")]
    [SerializeField] private TMP_Text charaterNameText;
    [Header("Hearts")]
    [SerializeField] private HeartUI heartUI;

    [Header("Stats")]
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text timeText;

    [Header("Active Object")]
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject noSaveObject;

    [Header("DeleteMode")]
    [SerializeField] private GameObject deleteMode;

    #region Update UI Real Time

    public void UpdateUI(GameData data, Sprite characterIcon)
    {
        if (data == null)
        {
            ShowNoSave();
            return;
        }

        content.SetActive(true);
        noSaveObject.SetActive(false);

        characterImage.sprite = characterIcon;
        charaterNameText.text = data.characterName;

        heartUI.Init(data.playerMaxHP);
        heartUI.UpdateHearts(data.playerHP);

        coinText.text = data.coin.ToString();
        timeText.text = FormatTime(data.playTime);
    }

    #endregion

    private void ShowNoSave()
    {
        content.SetActive(false);
        noSaveObject.SetActive(true);
    }

    #region Helper

    private string FormatTime(float time)
    {
        int hours = Mathf.FloorToInt(time / 3600);
        int minutes = Mathf.FloorToInt((time % 3600) / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        return $"{hours:00}:{minutes:00}:{seconds:00}";
    }

    #endregion

    #region DeleteMode

    public void SetDeleteMode(bool isDelete)
    {
        if (deleteMode != null)
        {
            deleteMode.SetActive(isDelete);
        }
    }

    #endregion
}