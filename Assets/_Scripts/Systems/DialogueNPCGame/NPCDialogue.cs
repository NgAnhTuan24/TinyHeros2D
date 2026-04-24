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

    [SerializeField] private Animator animator;

    private PlayerIdentity player;

    private bool isTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTrigger)
        {
            player = other.GetComponent<PlayerIdentity>();

            if (animator != null)
                animator.SetBool("On", true);

            StartCoroutine(RunDialogue());
        }
    }

    IEnumerator RunDialogue()
    {
        isTrigger = true;

        if (dialogue == null || dialogue.lines.Length == 0)
        {
            isTrigger = false;
            yield break;
        }

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

        if (animator != null)
            animator.SetBool("On", false);

        isTrigger = false;
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