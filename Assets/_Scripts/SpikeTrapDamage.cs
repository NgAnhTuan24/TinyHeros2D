using UnityEngine;

public class SpikeTrapDamage : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float damageCooldown = 1f;

    private float lastDamageTime;

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        if (player == null) return;

        if (Time.time < lastDamageTime + damageCooldown) return;

        player.TakeDamage(damage, transform);
        lastDamageTime = Time.time;
    }
}
