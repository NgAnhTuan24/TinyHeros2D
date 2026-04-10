using System.Collections;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("ID")]
    [SerializeField] private string dialogueID;

    [Header("Dialogue_1")]
    [SerializeField] private DialogueLine[] lines;

    [Header("Dialogue_2")]
    [SerializeField] private DialogueLine[] afterBattleLines;

    [SerializeField] private bool spawnAfterDialogue;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;

    [Header("Portal")]
    [SerializeField] private GameObject portalPrefab;
    [SerializeField] private Transform portalSpawnPoint;

    //private bool triggered = false;

    bool enemyHandled = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (triggered) return;

        if (collision.CompareTag("Player"))
        {
            if (GameManager.instance.dialogueStateManager.IsTriggered(dialogueID)) return;

            //triggered = true;

            if (spawnAfterDialogue)
            {
                DialogueManager.Instance.StartDialogue(lines, () =>
                {
                    GameManager.instance.dialogueStateManager.MarkTriggered(dialogueID);
                    SpawnEnemy();
                });
            }
            else
            {
                DialogueManager.Instance.StartDialogue(lines, () =>
                {
                    GameManager.instance.dialogueStateManager.MarkTriggered(dialogueID);
                });
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoint == null) return;

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        IEnemy enemyInterface = enemy.GetComponent<IEnemy>();

        if (enemyInterface != null)
        {
            enemyInterface.OnDie += OnEnemyDie;
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
            DialogueManager.Instance.StartDialogue(afterBattleLines, SpawnPortal);
        }
    }

    void SpawnPortal()
    {
        if (portalPrefab == null || portalSpawnPoint == null) return;

        Instantiate(portalPrefab, portalSpawnPoint.position, Quaternion.identity);
    }
}