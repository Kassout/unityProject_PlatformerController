public class HellDogPlayerDetectedState : PlayerDetectedState
{
    private HellDog _hellDog;
    
    public HellDogPlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, 
        PlayerDetectedStateData stateData, HellDog hellDog) 
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

        if (!_isPlayerInMaxAggroRange)
        {
            _hellDog.IdleState.SetFlipAfterIdle(true);
            _stateMachine.ChangeState(_hellDog.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
