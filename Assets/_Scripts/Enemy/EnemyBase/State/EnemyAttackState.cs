using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float attackCooldown = 1.5f;
    private float recoveryTime = .2f;

    private float timer;
    private bool isRecovering;

    public EnemyAttackState(EnemyController enemy) : base(enemy) { }

    public override void Enter()
    {
        timer = 0;
        isRecovering = false;

        enemy.Movement.Stop();
        enemy.Animator.SetMove(false);

        Attack();
    }

    public override void Update()
    {
        if (enemy.knockback.gettingKnockedBack) return;

        enemy.Movement.Stop();

        timer += Time.deltaTime;

        if (!isRecovering)
        {
            if (timer >= attackCooldown)
            {
                isRecovering = true;
                timer = 0;
            }
        }
        else
        {
            if (timer >= recoveryTime)
            {
                DecideNextState();
            }
        }
    }

    private void Attack()
    {
        enemy.Animator.TriggerAttack();
    }

    private void DecideNextState()
    {
        if (enemy.Detection.InAttackRange())
        {
            enemy.StateMachine.ChangeState(enemy.AttackState);
        }
        else if (enemy.Detection.CanSeePlayer())
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }
        else
        {
            enemy.StateMachine.ChangeState(enemy.UsePatrol ? enemy.PatrolState : enemy.IdleState);
        }
    }
}