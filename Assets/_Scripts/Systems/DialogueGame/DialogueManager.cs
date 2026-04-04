using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;

    [Header("Other UI")]
    [SerializeField] private GameObject[] otherUI;

    private DialogueLine[] lines;
    private int index;
    private bool isPlaying;

    private Coroutine typingCoroutine;
    private bool isTyping;
    [SerializeField] private float typingSpeed = 0.02f;

    private PlayerIdentity identity;

    private Action onDialogueEnd;

    private void Awake()
    {
        Instance = this;
    }

    public void StartDialogue(DialogueLine[] dialogueLines, Action onEnd = null)
    {
        PlayerController.instance.movement.LockPlayer();

        onDialogueEnd = onEnd;

        lines = dialogueLines;
        index = 0;
        isPlaying = true;

        panel.SetActive(true);

        foreach (var ui in otherUI)
            ui.SetActive(false);

        identity = FindObjectOfType<PlayerIdentity>();

        ShowLine();
    }

    void ShowLine()
    {
        if (index >= lines.Length)
        {
            EndDialogue();
            return;
        }

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(lines[index]));
    }

    IEnumerator TypeText(DialogueLine line)
    {
        isTyping = true;

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

    public void Next()
    {
        if (!isPlaying) return;

        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = lines[index].text;
            isTyping = false;
            return;
        }

        index++;
        ShowLine();
    }

    void EndDialogue()
    {
        isPlaying = false;
        panel.SetActive(false);

        foreach (var ui in otherUI)
            ui.SetActive(true);

        PlayerController.instance.movement.UnlockPlayer();

        onDialogueEnd?.Invoke();
    }
}