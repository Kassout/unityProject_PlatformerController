public class ArcherMoveState : MoveState
{
    private Archer _archer;
    
    public ArcherMoveState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, 
        MoveStateData stateData, Archer archer) 
        : base(entity, stateMachine, animationBoolName, stateData)
    {
        _archer = archer;
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

        if (_isPlayerInMinAggroRange)
        {
            _stateMachine.ChangeState(_archer.PlayerDetectedState);
        }
        else if (_isDetectingWall || !_isDetectingLedge)
        {
            _archer.IdleState.SetFlipAfterIdle(true);
            _stateMachine.ChangeState(_archer.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
