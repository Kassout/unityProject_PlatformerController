public class ArcherPlayerDetectedState : PlayerDetectedState
{
    private Archer _archer;
    
    public ArcherPlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, 
        PlayerDetectedStateData stateData, Archer archer) 
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

        if (_performCloseRangeAction)
        {
            _stateMachine.ChangeState(_archer.MeleeAttackState);
        }
        else if (!_isPlayerInMaxAggroRange)
        {
            _stateMachine.ChangeState(_archer.LookForPlayerState);
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
