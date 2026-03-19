using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    private float patrolTime = 3f;
    private float timer;

    public EnemyPatrolState(EnemyController enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        if (!enemy.UsePatrol)
        {
            enemy.StateMachine.ChangeState(enemy.IdleState);
            return;
        }

        timer = patrolTime;
        enemy.Movement.ResetPatrolTimer();
    }

    public override void Update()
    {
        enemy.Movement.Patrol();

        timer -= Time.deltaTime;

        if (enemy.Detection.CanSeePlayer())
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
            return;
        }

        if (timer <= 0)
        {
            enemy.StateMachine.ChangeState(enemy.IdleState);
        }
    }
}
