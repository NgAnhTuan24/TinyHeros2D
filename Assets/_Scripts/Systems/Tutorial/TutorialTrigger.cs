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

        var manager = GameManager.instance.tutorialStateManager;

        switch(type)
        {
            case TutorialType.Jump:
                UIManager.instance.controlUIManager.ShowJump();
                manager.jumpUnlocked = true;
                UIManager.instance.RefreshUpgradeUI();
                break;
            case TutorialType.Attack:
                UIManager.instance.controlUIManager.ShowAttack();
                manager.attackUnlocked = true;
                UIManager.instance.RefreshUpgradeUI();
                break;
            case TutorialType.Throw:
                UIManager.instance.controlUIManager.ShowThrow();
                manager.throwUnlocked = true;
                UIManager.instance.RefreshUpgradeUI();
                break;
        }

        UIManager.instance.tutorialDialogueUI.ShowDialogue(iconLeft, iconRight, message);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (UIManager.instance == null) return;

        UIManager.instance.tutorialDialogueUI.HideDialogue();
    }
}
