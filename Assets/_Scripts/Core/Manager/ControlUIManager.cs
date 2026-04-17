using UnityEngine;

public class ControlUIManager : MonoBehaviour
{
    public GameObject jumpButton;
    public GameObject attackButton;
    public GameObject throwButton;
    public GameObject interactButton;

    private IInteractable currentInteractable;

    public void ShowJump()
    {
        jumpButton.SetActive(true);
    }

    public void ShowAttack()
    {
        attackButton.SetActive(true);
    }

    public void ShowThrow()
    {
        throwButton.SetActive(true);
    }

    public void ShowInteractButton(IInteractable interactable)
    {
        interactButton.SetActive(true);
        currentInteractable = interactable;
    }

    public void HideInteractButton(IInteractable interactable)
    {
        if (interactable == null || currentInteractable == null)
        {
            return;
        }

        if (currentInteractable == interactable)
        {
            interactButton.SetActive(false);
            currentInteractable = null;
        }
    }

    public void OnInteractButtonPressed()
    {
        currentInteractable?.Interact();
    }
}
