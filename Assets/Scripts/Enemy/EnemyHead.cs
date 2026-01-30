using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    [SerializeField] EnemyPatrol enemy;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        enemy.Die();
    }
}