using UnityEngine;

public class UISaveButton : MonoBehaviour
{
    public void Save()
    {
        GameManager.instance.SaveGame();
    }
}