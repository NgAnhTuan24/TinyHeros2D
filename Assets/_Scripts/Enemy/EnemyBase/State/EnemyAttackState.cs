using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float cooldown = 1.5f;
    private float timer;

    public EnemyAttackState(EnemyController enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        timer = cooldown;
        enemy.Movement.Stop();

        enemy.Animator.SetMove(false);
        enemy.Animator.TriggerAttack();
    }

    public override void Update()
    {
        if (enemy.knockback.gettingKnockedBack) return;

        timer -= Time.deltaTime;

        if (!enemy.Detection.InAttackRange())
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
            return;
        }
        if (timer <= 0)
        {
            timer = cooldown;
            enemy.Animator.TriggerAttack();
        }
    }
}
