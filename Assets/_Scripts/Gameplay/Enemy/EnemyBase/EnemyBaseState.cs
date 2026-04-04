public abstract class EnemyBaseState
{
    protected EnemyController enemy;

    public EnemyBaseState(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
