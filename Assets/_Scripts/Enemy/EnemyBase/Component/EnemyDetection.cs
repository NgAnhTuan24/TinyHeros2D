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
}
