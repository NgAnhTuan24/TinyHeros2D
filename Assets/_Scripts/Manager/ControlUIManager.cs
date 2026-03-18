using UnityEngine;

public class ControlUIManager : MonoBehaviour
{
    public GameObject jumpButton;
    public GameObject attackButton;
    public GameObject throwButton;

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
}
