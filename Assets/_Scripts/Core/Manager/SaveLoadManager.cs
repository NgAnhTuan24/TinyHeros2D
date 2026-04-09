using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    public static GameData currentData;

    [SerializeField] private string sceneName = "SelectCharacter";

    public static int selectedSlot = -1;

    [SerializeField] private SaveSlotUI slot1;
    [SerializeField] private SaveSlotUI slot2;

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
        selectedSlot = slotIndex;

        Debug.Log("Chọn slot: " + slotIndex);

        GameData data = SaveSystem.Load(slotIndex);

        if (data == null)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            currentData = data;
            SceneManager.LoadScene(data.sceneName);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}