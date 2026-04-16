using System;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private DialogueLine[] lines;
    private int index;
    private bool isPlaying;

    private Action onDialogueEnd;

    public void StartDialogue(DialogueLine[] dialogueLines, Action onEnd = null)
    {
        PlayerController.instance.movement.LockPlayer();

        onDialogueEnd = onEnd;

        lines = dialogueLines;
        index = 0;
        isPlaying = true;

        UIManager.instance.dialogueUI.ShowPanel(true);
        UIManager.instance.dialogueUI.ToggleOtherUI(false);

        ShowLine();
    }

    void ShowLine()
    {
        if (index >= lines.Length)
        {
            EndDialogue();
            return;
        }

        UIManager.instance.dialogueUI.ShowLine(lines[index]);
    }

    public void Next()
    {
        if (!isPlaying) return;

        if (UIManager.instance.dialogueUI.IsTyping())
        {
            UIManager.instance.dialogueUI.SkipTyping();
            return;
        }

        index++;
        ShowLine();
    }

    void EndDialogue()
    {
        isPlaying = false;

        UIManager.instance.dialogueUI.ShowPanel(false);
        UIManager.instance.dialogueUI.ToggleOtherUI(true);

        PlayerController.instance.movement.UnlockPlayer();

        onDialogueEnd?.Invoke();
    }
}