using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueLine[] lines;

    [SerializeField] private bool spawnAfterDialogue;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;

    private bool triggered = false;

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

        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}