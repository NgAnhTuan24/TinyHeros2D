using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int dmg;

    private void OnTriggerEnter2D(Collider2D col)
    {
        PlayerHealth pl = col.gameObject.GetComponent<PlayerHealth>();

        if (pl != null)
        {
            pl.TakeDamage(dmg, transform);
        }
    }
}