using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    [SerializeField] private Transform playerPos;

    public EnemyState CurrentState { get; private set; }

    private EnemyMovement movement;
    private EnemyAnimator animator;
    private EnemyAttack attack;
    private Knockback knockback;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        animator = GetComponent<EnemyAnimator>();
        attack = GetComponent<EnemyAttack>();
        knockback = GetComponent<Knockback>();
        movement.SetGravity(data.gravityScale);
    }

    private void Update()
    {
        if (playerPos == null) 
        { 
            FindPlayer(); 
            return; 
        }

        UpdateState();
        ExecuteState();
    }

    private void UpdateState()
    {
        if (knockback.gettingKnockedBack)
        {
            return;
        }

        if (attack.IsAttacking) return;

        float distance = Vector2.Distance(transform.position, playerPos.position);

        if (distance <= data.attackRange)
        {
            ChangeState(EnemyState.Attack);
        }
        else if (distance <= data.chaseRange)
        {
            ChangeState(EnemyState.Chase);
        }
        else
        {
            ChangeState(EnemyState.Idle);
        }
    }

    private void ExecuteState()
    {
        if (knockback.gettingKnockedBack) return;

        switch (CurrentState)
        {
            case EnemyState.Idle:
                movement.Stop();
                break;

            case EnemyState.Chase:
                movement.Chase(playerPos.position, data.moveSpeed);
                break;

            case EnemyState.Attack:
                movement.Stop();
                attack.TryAttack();
                break;

            case EnemyState.Hurt:
                break;
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        animator.SetState(newState);
    }

    void FindPlayer()
    {
        if (Init.plTransform != null)
        {
            playerPos = Init.plTransform;
            return;
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) playerPos = playerObj.transform;
    }

    private void OnDrawGizmosSelected()
    {
        if (data == null) return;

        // Chase range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, data.chaseRange);

        // Attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, data.attackRange);

        //Attack hit
        if (attack != null && attack.AttackPoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(attack.AttackPoint.position, data.attackHitRadius);
        }
    }
}
