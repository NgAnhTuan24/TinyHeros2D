using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int damage = 1;

    [Header("Attack Point")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 0.5f;
    [SerializeField] private LayerMask playerLayer;

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator DoAttack()
    {
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(3f);
    }

    public void DealDamage()
    {
        Collider2D hit = Physics2D.OverlapCircle(
           attackPoint.position,
           attackRadius,
           playerLayer
       );

        if (hit != null)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage, transform);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}