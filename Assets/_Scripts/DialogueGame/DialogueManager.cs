using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text text;

    [Header("Other UI")]
    [SerializeField] private GameObject[] otherUI;

    private DialogueLine[] lines;
    private int index;
    private bool isPlaying;

    private Coroutine typingCoroutine;
    private bool isTyping;
    [SerializeField] private float typingSpeed = 0.02f;

    private void Awake()
    {
        Instance = this;
    }

    public void StartDialogue(DialogueLine[] dialogueLines)
    {
        PlayerController.instance.movement.LockPlayer();

        lines = dialogueLines;
        index = 0;
        isPlaying = true;

        panel.SetActive(true);

        // Ẩn UI khác
        foreach (var ui in otherUI)
            ui.SetActive(false);

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

        icon.sprite = line.icon;
        text.text = "";

        foreach (char c in line.text)
        {
            text.text += c;
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
            text.text = lines[index].text;
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
    }
}