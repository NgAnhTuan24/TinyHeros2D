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

    [SerializeField] private bool allowReplayEnemy = true;
    private bool enemyAlive = false;

    private bool triggered = false;

    bool enemyHandled = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;
        if (!collision.CompareTag("Player")) return;

        var state = GameManager.instance.dialogueStateManager;

        if (state.IsCompleted(dialogueID))
        {
            if (allowReplayEnemy)
            {
                SpawnEnemy();
            }
            return;
        }

        triggered = true;

        if (!state.IsStarted(dialogueID))
        {
            GameManager.instance.dialogueManager.StartDialogue(lines, () =>
            {
                state.MarkStarted(dialogueID);
                SpawnEnemy();
            });
        }
        else
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoint == null) return;

        if (enemyAlive) return;

        enemyHandled = false;

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemyAlive = true;

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

        //enemyAlive = false;

        var state = GameManager.instance.dialogueStateManager;

        if (!state.IsCompleted(dialogueID))
        {
            state.MarkCompleted(dialogueID);
            StartCoroutine(PlayAfterDialogue());
        }
        else
        {
            SpawnPortal();
        }
    }

    IEnumerator PlayAfterDialogue()
    {
        yield return new WaitForSeconds(0.5f);

        if (afterBattleLines != null && afterBattleLines.Length > 0)
        {
            GameManager.instance.dialogueManager.StartDialogue(afterBattleLines, SpawnPortal);
        }
    }

    void SpawnPortal()
    {
        if (portalPrefab == null || portalSpawnPoint == null) return;

        Instantiate(portalPrefab, portalSpawnPoint.position, Quaternion.identity);
    }
}