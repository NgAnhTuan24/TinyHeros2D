using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 1;

    [SerializeField] private float damageCooldown = 1f;
    private float lastDamageTime;

    void OnCollisionStay2D(Collision2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player == null) return;

        if (Time.time < lastDamageTime + damageCooldown) return;

        player.TakeDamage(damage, transform);
        lastDamageTime = Time.time;
    }
}