using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Character player;

    void Start()
    {
        player = Init.player;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(player.Damage, transform);
        }
    }
}