using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private float idleTime = 2f;
    private float timer;

    public EnemyIdleState(EnemyController enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        timer = idleTime;
        enemy.Movement.Stop();

        enemy.Animator.SetMove(false);
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if (enemy.Detection.CanSeePlayer())
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
            return;
        }

        if (timer <= 0)
        {
            if (enemy.UsePatrol)
            {
                enemy.StateMachine.ChangeState(enemy.PatrolState);
            }
            else
            {
                timer = idleTime;
            }
        }
    }
}
