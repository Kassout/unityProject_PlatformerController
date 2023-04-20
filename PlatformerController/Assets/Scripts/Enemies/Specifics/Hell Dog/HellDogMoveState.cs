public class HellDogMoveState : MoveState
{
    private HellDog _hellDog;
    
    public HellDogMoveState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, MoveStateData stateData, HellDog hellDog) 
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

        if (_isDetectingWall || !_isDetectingLedge)
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
