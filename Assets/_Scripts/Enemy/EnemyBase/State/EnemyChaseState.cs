public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(EnemyController enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        enemy.Animator.SetMove(true);
    }

    public override void Update()
    {
        enemy.Movement.ChasePlayer();

        if (!enemy.Detection.CanSeePlayer())
        {
            if (enemy.UsePatrol)
                enemy.StateMachine.ChangeState(enemy.PatrolState);
            else
                enemy.StateMachine.ChangeState(enemy.IdleState);

            return;
        }

        if (enemy.Detection.InAttackRange())
        {
            enemy.StateMachine.ChangeState(enemy.AttackState);
        }
    }
}
