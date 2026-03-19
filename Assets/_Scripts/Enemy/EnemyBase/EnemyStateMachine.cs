public class EnemyStateMachine
{
   public EnemyBaseState CurrentState { get; private set; }

    public void Initialize(EnemyBaseState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(EnemyBaseState newState)
    {
        if (CurrentState == newState) return;

        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
