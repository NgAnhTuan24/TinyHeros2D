using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask playerLayer;

    private float lastAttackTime;
    private EnemyAnimator enemyAnimator;
    private Knockback knockback;


    public Transform AttackPoint => attackPoint;
    public bool IsAttacking { get; private set; }

    private Coroutine attackRoutine;

    private void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        knockback = GetComponent<Knockback>();
    }

    public void TryAttack()
    {
        if (knockback.gettingKnockedBack) return;
        if (IsAttacking) return;

        if (Time.time < lastAttackTime + data.attackCooldown)
            return;

        lastAttackTime = Time.time;
        IsAttacking = true;

        enemyAnimator.TriggerAttack();

        if (attackRoutine != null)
            StopCoroutine(attackRoutine);

        attackRoutine = StartCoroutine(AttackCooldownRoutine());
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(.5f);
        IsAttacking = false;
    }

    public void EndAttack()
    {
        IsAttacking = false;
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
}
