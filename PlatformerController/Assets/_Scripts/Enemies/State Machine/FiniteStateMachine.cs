public class FiniteStateMachine
{
    public State CurrentState { get; private set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
