using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public TutorialType type;

    public Sprite iconLeft;
    public Sprite iconRight;

    [TextArea]
    public string message;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        switch(type)
        {
            case TutorialType.Jump:
                UIManager.instance.controlUIManager.ShowJump();
                break;
            case TutorialType.Attack:
                UIManager.instance.controlUIManager.ShowAttack();
                break;
            case TutorialType.Throw:
                UIManager.instance.controlUIManager.ShowThrow();
                break;
        }

        UIManager.instance.tutorialDialogueUI.ShowDialogue(iconLeft, iconRight, message);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        UIManager.instance.tutorialDialogueUI.HideDialogue();
    }
}
