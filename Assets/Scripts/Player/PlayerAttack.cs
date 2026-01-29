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
        IDamageable target = other.GetComponent<IDamageable>();

        if (target != null)
        {
            target.TakeDamage(player.Damage);
        }
    }
}
