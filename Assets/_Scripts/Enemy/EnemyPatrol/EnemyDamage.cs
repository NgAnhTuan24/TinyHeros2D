using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int dmg;

    [SerializeField] private float damageCooldown = 1f;
    private float lastDamageTime;

    void OnCollisionStay2D(Collision2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player == null) return;

        if (Time.time < lastDamageTime + damageCooldown) return;

        player.TakeDamage(1, transform);
        lastDamageTime = Time.time;
    }
}