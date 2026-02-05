using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask playerLayer;

    private float lastAttackTime;
    private EnemyAnimator enemyAnimator;
    private Knockback knockback;

    private void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        knockback = GetComponent<Knockback>();
    }

    public void TryAttack()
    {
        if (knockback.gettingKnockedBack) return;

        if (Time.time < lastAttackTime + data.attackCooldown)
            return;

        lastAttackTime = Time.time;
        enemyAnimator.TriggerAttack();
    }

    public void DealDamage()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            attackPoint.position,
            data.attackHitRadius,
            playerLayer
        );

        if (hit != null)
        {
            hit.GetComponent<PlayerHealth>()?.TakeDamage(data.attackDmg,transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, data.attackHitRadius);
    }
}
