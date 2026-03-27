using System.Collections;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue_1")]
    [SerializeField] private DialogueLine[] lines;

    [Header("Dialogue_2")]
    [SerializeField] private DialogueLine[] afterBattleLines;

    [SerializeField] private bool spawnAfterDialogue;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;

    private bool triggered = false;

    bool enemyHandled = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;

        if (collision.CompareTag("Player"))
        {
            triggered = true;

            if (spawnAfterDialogue)
            {
                DialogueManager.Instance.StartDialogue(lines, SpawnEnemy);
            }
            else
            {
                DialogueManager.Instance.StartDialogue(lines);
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoint == null) return;

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        EnemyHealth health = enemy.GetComponent<EnemyHealth>();

        if (health != null)
        {
            health.onDie += OnEnemyDie;
        }
    }

    void OnEnemyDie()
    {
        if (enemyHandled) return;
        enemyHandled = true;

        if (afterBattleLines == null || afterBattleLines.Length == 0) return;

        StartCoroutine(PlayAfterDialogue());
    }

    IEnumerator PlayAfterDialogue()
    {
        yield return new WaitForSeconds(0.5f);

        if (afterBattleLines != null && afterBattleLines.Length > 0)
        {
            DialogueManager.Instance.StartDialogue(afterBattleLines);
        }
    }
}