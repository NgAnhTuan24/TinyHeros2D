using UnityEngine;

public class EnemyDmage : MonoBehaviour
{
    [SerializeField] private int dmg = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth pl = collision.GetComponent<PlayerHealth>();
            if (pl != null)
            {
                pl.TakeDamage(dmg, transform);
            }
        }
    }
}
