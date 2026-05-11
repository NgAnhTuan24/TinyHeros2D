using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    public static GameData currentData;

    [SerializeField] private string sceneName = "SelectCharacter";

    public static int selectedSlot = -1;

    [SerializeField] private SaveSlotUI slot1;
    [SerializeField] private SaveSlotUI slot2;

    public bool isDeleteMode = false;

    void Start()
    {
        LoadSlotUI(0, slot1);
        LoadSlotUI(1, slot2);
    }

    void LoadSlotUI(int slotIndex, SaveSlotUI slotUI)
    {
        GameData data = SaveSystem.Load(slotIndex);

        if (data == null)
        {
            slotUI.UpdateUI(null, null);
            return;
        }

        Sprite icon = CharacterData.instance.GetIcon(data.characterName);

        slotUI.UpdateUI(data, icon);
    }

    public void SelectSlot(int slotIndex)
    {
        Debug.Log("Chọn slot: " + slotIndex);

        if (isDeleteMode)
        {
            DeleteSlot(slotIndex);
            return;
        }

        selectedSlot = slotIndex;

        GameData data = SaveSystem.Load(slotIndex);

        if (data == null)
        {
            currentData = null;
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            currentData = data;
            SceneManager.LoadScene(data.sceneName);
        }
    }

    public void ToggleDeleteMode()
    {
        isDeleteMode = !isDeleteMode;

        Debug.Log("Delete Mode: " + isDeleteMode);

        slot1.SetDeleteMode(isDeleteMode);
        slot2.SetDeleteMode(isDeleteMode);
    }

    void DeleteSlot(int slotIndex)
    {
        SaveSystem.Delete(slotIndex);

        if (selectedSlot == slotIndex)
        {
            currentData = null;
        }

        Debug.Log("Đã xóa slot: " + slotIndex);

        if (slotIndex == 0)
            LoadSlotUI(0, slot1);
        else if (slotIndex == 1)
            LoadSlotUI(1, slot2);

        ToggleDeleteMode();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}