using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private Transform player;

    private EnemyController enemy;

    private void Awake()
    {
        enemy = GetComponent<EnemyController>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public bool CanSeePlayer()
    {
        if (player == null) return false;

        float distance = Vector2.Distance(transform.position, player.position);
        return distance <= enemy.DetectRange;
    }

    public bool InAttackRange()
    {
        if (player == null) return false;

        float distance = Vector2.Distance(transform.position, player.position);
        return distance <= enemy.AttackRange;
    }

    public Transform GetPlayer()
    {
        return player;
    }

    private void OnDrawGizmosSelected()
    {
        if (enemy == null)
            enemy = GetComponent<EnemyController>();

        if (enemy == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemy.DetectRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemy.AttackRange);

        if (Application.isPlaying && player != null)
        {
            Vector2 dir = (player.position - transform.position).normalized;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(
                transform.position,
                transform.position + (Vector3)(dir * enemy.DetectRange)
            );
        }
    }
}
