using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetState(EnemyState state)
    {
        animator.SetBool("IsIdle", state == EnemyState.Idle);
        animator.SetBool("IsChasing", state == EnemyState.Chase);
        animator.SetBool("IsAttacking", state == EnemyState.Attack);
        animator.SetBool("IsHurt", state == EnemyState.Hurt);
    }

    public void TriggerAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void TriggerHurt()
    {
        animator.SetTrigger("Hurt");
    }
}
