using UnityEngine;
using TMPro;
using System.Collections;

public class NPCDialogue : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject bubble;
    [SerializeField] private TMP_Text text;

    [Header("Data")]
    [SerializeField] private DialogueData dialogue;

    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float delayBetweenLines = 2f;

    private Coroutine dialogueRoutine;
    private PlayerIdentity player;

    private void Start()
    {
        player = FindObjectOfType<PlayerIdentity>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerIdentity>();

            dialogueRoutine = StartCoroutine(RunDialogue());
        }
    }

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        if (dialogueRoutine != null)
    //            StopCoroutine(dialogueRoutine);

    //        bubble.SetActive(false);
    //    }
    //}

    IEnumerator RunDialogue()
    {
        if (dialogue == null || dialogue.lines.Length == 0)
            yield break;

        bubble.SetActive(true);

        foreach (string line in dialogue.lines)
        {
            string finalLine = line;

            if (player != null)
            {
                finalLine = finalLine.Replace("{player}", player.playerName);
            }

            yield return StartCoroutine(TypeLine(finalLine));

            yield return new WaitForSeconds(delayBetweenLines);
        }

        bubble.SetActive(false);
    }

    IEnumerator TypeLine(string line)
    {
        text.text = "";

        foreach (char c in line)
        {
            text.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}