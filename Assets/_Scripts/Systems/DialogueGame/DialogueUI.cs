using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;

    [Header("Other UI")]
    [SerializeField] private GameObject[] otherUI;

    [SerializeField] private float typingSpeed = 0.02f;

    private Coroutine typingCoroutine;
    private bool isTyping;

    private PlayerIdentity identity;
    private DialogueLine currentLine;

    public void ShowPanel(bool show)
    {
        panel.SetActive(show);
    }

    public void ToggleOtherUI(bool show)
    {
        foreach (var ui in otherUI)
            ui.SetActive(show);
    }

    public void ShowLine(DialogueLine line)
    {
        currentLine = line;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(line));
    }

    IEnumerator TypeText(DialogueLine line)
    {
        isTyping = true;

        if (identity == null)
            identity = FindObjectOfType<PlayerIdentity>();

        if (line.speaker == SpeakerType.Player && identity != null)
        {
            icon.sprite = identity.playerIconDialogue;
            nameText.text = identity.playerName;
        }
        else
        {
            icon.sprite = line.icon;
            nameText.text = line.name;
        }

        dialogueText.text = "";

        foreach (char c in line.text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public bool IsTyping()
    {
        return isTyping;
    }

    public void SkipTyping()
    {
        if (!isTyping) return;

        StopCoroutine(typingCoroutine);
        dialogueText.text = currentLine.text;
        isTyping = false;
    }
}
