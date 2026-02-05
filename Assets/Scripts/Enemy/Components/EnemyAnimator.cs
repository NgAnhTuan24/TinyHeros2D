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
    }

    public void TriggerAttack()
    {
        animator.SetTrigger("Attack");
    }
}
