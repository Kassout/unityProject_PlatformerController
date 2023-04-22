public class ArcherIdleState : IdleState
{
    private Archer _archer;
    
    public ArcherIdleState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, 
        IdleStateData stateData, Archer archer) 
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
        else if (_isIdleTimeOver)
        {
            _stateMachine.ChangeState(_archer.MoveState);
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
