using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    public static int selectedSlot = -1;

    [SerializeField] private SaveSlotUI slot1;
    [SerializeField] private SaveSlotUI slot2;

    void Start()
    {
        slot1.UpdateUI(null, null);
        slot2.UpdateUI(null, null);
    }

    public void SelectSlot(int slotIndex)
    {
        selectedSlot = slotIndex;

        Debug.Log("Chọn slot: " + slotIndex);

        SceneManager.LoadScene(2);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}