public class HellDogIdleState : IdleState
{
    private HellDog _hellDog;
    
    public HellDogIdleState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, IdleStateData stateData, HellDog hellDog) 
        : base(entity, stateMachine, animationBoolName, stateData)
    {
        _hellDog = hellDog;
    }
    
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isIdleTimeOver)
        {
            _stateMachine.ChangeState(_hellDog.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }   
}
