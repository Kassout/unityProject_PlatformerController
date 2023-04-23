public class ArcherDodgeState : DodgeState
{
    private Archer _archer;
    
    public ArcherDodgeState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, 
        DodgeStateData stateData, Archer archer) 
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

        if (_isDodgeOver)
        {
            if (_isPlayerInMaxAggroRange && _performCloseRangeAction)
            {
                _stateMachine.ChangeState(_archer.MeleeAttackState);
            }
            else if (_isPlayerInMaxAggroRange && !_performCloseRangeAction)
            {
                _stateMachine.ChangeState(_archer.RangedAttackState);
            }
            else if (!_isPlayerInMaxAggroRange)
            {
                _stateMachine.ChangeState(_archer.LookForPlayerState);
            }
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
