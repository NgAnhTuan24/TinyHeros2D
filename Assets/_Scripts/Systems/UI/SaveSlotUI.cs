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
    [Header("Active Object")]
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject noSaveObject;

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
    }

    private void ShowNoSave()
    {
        content.SetActive(false);
        noSaveObject.SetActive(true);
    }
}