using UnityEngine;

public class InteractZone : MonoBehaviour
{
    private IInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<IInteractable>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            UIManager.instance.controlUIManager.ShowInteractButton(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            UIManager.instance.controlUIManager.HideInteractButton(interactable);
        }
    }
}