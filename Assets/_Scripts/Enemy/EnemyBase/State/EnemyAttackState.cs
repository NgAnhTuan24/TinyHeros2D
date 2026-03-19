using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float cooldown = 1f;
    private float timer;

    public EnemyAttackState(EnemyController enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        timer = cooldown;
        enemy.Movement.Stop();
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if (!enemy.Detection.InAttackRange())
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
            return;
        }
        if (timer <= 0)
        {
            enemy.Combat.Attack();
            timer = cooldown;
        }
    }
}
