using UnityEngine;

public class ControlUIManager : MonoBehaviour
{
    public GameObject jumpButton;
    public GameObject attackButton;
    public GameObject throwButton;
    public GameObject interactButton;

    private AreaExit currentExit;

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

    public void ShowInteractButton(AreaExit exit)
    {
        interactButton.SetActive(true);
        currentExit = exit;
    }

    public void HideInteractButton()
    {
        interactButton.SetActive(false);
        currentExit = null;
    }

    public void OnInteractButtonPressed()
    {
        currentExit?.TryInteract();
    }
}
