using UnityEngine;

public class UIInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private UIType uiType;

    public void Interact()
    {
        UIManager.instance.ToggleUI(uiType);
    }
}